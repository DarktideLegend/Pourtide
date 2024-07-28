#!/bin/bash

# Get the current working directory
directory=$(pwd)

# Find all .sql files and replace ' 25)' with ' 100)'
find "$directory" -name '*.sql' -type f -exec sed -i.bak 's/ 25)/ 100)/g' {} +

echo "Update completed."

