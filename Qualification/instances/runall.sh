for i in *.in; do
    [ -f "$i" ] || break
    dotnet ../Qualification/bin/Debug/netcoreapp2.0/Qualification.dll $i > $i.out
done
