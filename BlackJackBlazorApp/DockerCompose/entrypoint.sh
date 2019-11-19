#!/bin/bash
echo "Starting Sql Server"
/opt/mssql/bin/sqlServer &
sleep 20 | echo "Initializing Database after 20 seconds of wait."

if [ ./data/*bak ]; then
    echo "Database Restore Starting"
    ./restoreDatabase.sh & 
    bash
else
    /var/opt/mssql/backup/createDatabase.sh
    sleep 10 | echo "Importing SQL Data into Databases."
    ./import-data.sh &
    bash
fi