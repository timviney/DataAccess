cd ..\src
docker build -t dataaccess .
cd ..\test

docker run -p 9000:8080 dataaccess
