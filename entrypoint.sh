#!/bin/bash
 
# Start SQL Server in the background
/opt/mssql/bin/sqlservr &
 
# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q "SELECT 1" &>/dev/null
do
  sleep 1
done
 
# Debug: List contents of /migration directory
echo "Listing /migration directory contents:"
ls -l /migration
 
# Run the migration script
echo "Running migration script..."
 
if /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i /migration/db.sql; then
  echo "db.sql executed successfully."
else
  echo "Error executing db.sql" >&2
  exit 1
fi
 
DB_NAME="DBColProfessional"
if /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d $DB_NAME -i /migration/migration.sql; then
  echo "migration.sql executed successfully."
else
  echo "Error executing migration.sql" >&2
  exit 1
fi
 
# Wait for all background processes
wait
