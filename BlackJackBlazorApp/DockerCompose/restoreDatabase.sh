echo "Start Restoring Database"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -1 "./restoreDatabase.sql"