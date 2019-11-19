#!/bin/sh

echo "Start Importing Data"
for filename in ./data/*.sql;
do
    [ -e "$filename"] || continue
    echo "Importing Data From" "$filename"
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i "$filename"
done