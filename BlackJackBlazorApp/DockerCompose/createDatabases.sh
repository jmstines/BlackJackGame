#!/usr/bin/env bash

echo "Creating Databases."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -1 "./databaseList.sql"