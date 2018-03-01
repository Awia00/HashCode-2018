﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Windemann.HashCode.Qualification.Heuristics;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public class QualificationSolverBandB : IQualificationSolver
    {
        private readonly QualificationInstance _instance;
        private readonly QualificationSolverSingleVehicle _upperHeuristic;
        private readonly QualificationSolverGreedy _lowerHeuristic;
        private readonly CancellationToken _cancellationToken;

        public QualificationSolverBandB(QualificationInstance instance, CancellationToken cancellationToken)
        {
            _instance = instance;
            _upperHeuristic = new QualificationSolverSingleVehicle(_instance);
            _lowerHeuristic = new QualificationSolverGreedy(_instance);
            _cancellationToken = cancellationToken;
        }
        
        
        public QualificationResult Solve()
        {
            var vehicles = new List<Vehicle>();
            for (var i = 0; i < _instance.NumberOfVehicles; i++)
            {
                vehicles.Add(new Vehicle());
            }
            var ridesLeft = new SortedSet<Ride>(_instance.Rides, new RideComparer());

            var root = new BbNode(_instance.NumberOfVehicles);
            root.Vehicles = vehicles;
            root.Rides = ridesLeft;
            root.LowerBound = LowerBound(root);
            root.UpperBound = UpperBound(root);

            var priorityQueue = new SortedSet<BbNode>(new BbNodeComparer());
            var bestNode = root;
            var incumbent = root.LowerBound;

            priorityQueue.Add(root);
            var lastIteration = 0;
            var iterations = 0;
            while (priorityQueue.Any() && iterations - lastIteration < 100)
            {
                iterations++;
                Console.Error.Write($"Number of node: {priorityQueue.Count}\r");
                var node = priorityQueue.Min;
                priorityQueue.Remove(node);
                
                if(node.UpperBound <= incumbent)
                    continue;

                foreach (var child in GetChildren(node))
                {
                    child.UpperBound = UpperBound(child);
                    child.LowerBound = LowerBound(child);
                    
                    if (child.LowerBound > incumbent)
                    {
                        incumbent = child.LowerBound;
                        bestNode = child;
                        
                        Console.Error.WriteLine($"New incumbent: {incumbent}                    ");
                        lastIteration = iterations;
                    }
                    
                    if (!MustBePruned(child, incumbent))
                        priorityQueue.Add(child);
                }
            }

            var result = new QualificationResult(_instance);
            foreach (var bestNodeAssignment in bestNode.Assignments)
            {
                result.AddAssignment(bestNodeAssignment.VehicleId, bestNodeAssignment.RideId);
            }

            foreach (var valueTuple in _lowerHeuristic.Solve(bestNode.Vehicles, bestNode.Rides, bestNode.Conflicts))
            {
                result.AddAssignment(valueTuple.VehicleId, valueTuple.RideId);
            }

            return result;
        }

        private bool MustBePruned(BbNode node, int incumbent)
        {
            if (node.UpperBound <= incumbent)
                return true;
            if (node.UpperBound == node.LowerBound)
                return true;
            // maybe more
            return false;
        }


        private IEnumerable<BbNode> GetChildren(BbNode node)
        {
            Ride pickedRide = null;
            Vehicle pickedVehicle = null;
            foreach (var nodeVehicle in node.Vehicles.AsParallel().OrderBy(x => x.TimeAvailable))
            {
                var ride = node.Rides.FirstOrDefault(x => nodeVehicle.CanPickup(x, _instance.NumberOfSteps)
                    && !node.Conflicts[nodeVehicle.Id].Contains(x.Id));
                if (ride != null)
                {
                    pickedVehicle = nodeVehicle;
                    pickedRide = ride;
                    break;
                }
            }

            if (pickedRide == null)
            {
                return Enumerable.Empty<BbNode>();
            }

            var stay = new BbNode(node);
            var take = new BbNode(node);

            stay.Conflicts[pickedVehicle.Id].Add(pickedRide.Id);

            take.Rides.Remove(pickedRide);
            take.Vehicles.Remove(pickedVehicle);
            take.Vehicles.Add(new Vehicle(pickedVehicle.Id, pickedRide.End, pickedVehicle.PossiblePickupTime(pickedRide) + pickedRide.Distance));

            take.Assignments.Add(new Assignment
            {
                RideId = pickedRide.Id,
                VehicleId = pickedVehicle.Id,
                Value = pickedRide.Score(_instance, pickedVehicle.PossiblePickupTime(pickedRide))
            });

            return new []{take, stay};
        }

        private int UpperBound(BbNode node)
        {
            return node.Vehicles.Sum(vehicle => _lowerHeuristic.Solve(new [] { vehicle }, node.Rides, node.Conflicts).Sum(r => r.Score)) + node.Assignments.Sum(x => x.Value);
        }

        private int LowerBound(BbNode node)
        {
            return _lowerHeuristic.Solve(node.Vehicles, node.Rides, node.Conflicts).Sum(r => r.Score) + node.Assignments.Sum(x => x.Value);
        }
    }
}
