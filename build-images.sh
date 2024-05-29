version=v2

docker build . --file ./src/Services/Catalog/Catalog.APi/Dockerfile -t wilsonnetodev/demo-store-catalog:$version
docker tag wilsonnetodev/demo-store-catalog:$version wilsonnetodev/demo-store-catalog:latest

docker push wilsonnetodev/demo-store-catalog:$version
docker push wilsonnetodev/demo-store-catalog:latest
