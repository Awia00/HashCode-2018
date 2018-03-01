using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windemann.HashCode.Qualification.Model;

namespace Windemann.HashCode.Qualification
{
    public class QualificationSolverBandB : IQualificationSolver
    {
        public QualificationResult Solve(QualificationInstance instance)
        {
            var root = new BbNode();
            root.LowerBound = LowerBound(root);
            root.UpperBound = UpperBound(root);

            var priorityQueue = new SortedSet<BbNode>();
            var bestNode = root;
            var incumbent = 0;

            priorityQueue.Add(root);
            while (priorityQueue.Any())
            {
                var node = priorityQueue.Min;
                priorityQueue.Remove(node);
                
                if(node.UpperBound <= incumbent)
                    continue;

                var (take, stay) = getChildren(node);
                take.UpperBound = UpperBound(take);
                take.LowerBound = LowerBound(take);
                stay.UpperBound = UpperBound(stay);
                stay.LowerBound = LowerBound(stay);

                if (node.LowerBound > incumbent)
                {
                    incumbent = node.LowerBound;
                    bestNode = node;
                }

                if (!MustBePruned(take, incumbent))
                    priorityQueue.Add(take);

                if (!MustBePruned(stay, incumbent))
                    priorityQueue.Add(stay);
            }

            var result = new QualificationResult(instance);
            foreach (var bestNodeAssignment in bestNode.Assignments)
            {
                result.AddAssignment(bestNodeAssignment.VehicleId, bestNodeAssignment.RideId);
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

        private (BbNode node1, BbNode node2) getChildren(BbNode node)
        {
            // pick a ride
            return (node, node);
        }

        private int UpperBound(BbNode node)
        {
            
            return 0;
        }

        private int LowerBound(BbNode node)
        {
            return 0;
        }
    }
}
