dotnet build .\KutnoAPI\KutnoAPI.csproj -c debug
dotnet publish .\KutnoAPI\KutnoAPI.csproj -c debug --no-restore -o "./Published"
docker build -t kutno-api .
docker run -d -p 8080:80 --name kutno-api-container kutno-api

docker save -o "KutnoAPI.tar" kutno-api
scp -r "KutnoAPI.tar" gajos@146.59.35.43:~

