cd ..\src

docker build -t dataaccess .

docker run -p 9000:8080 dataaccess