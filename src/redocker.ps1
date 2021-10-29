
echo "--------------------------------"
echo "Stopping and removing ms containers"
echo "--------------------------------"
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down

echo "--------------------------------"
echo "Building ms images"
echo "--------------------------------"
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml build

echo "--------------------------------"
echo "Running ms images"
echo "--------------------------------"
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d