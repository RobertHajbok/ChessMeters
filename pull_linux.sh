#!/bin/bash

date
PULL=`git pull`

if [ "$PULL" = "Already up to date." ]
then
  echo "Nothing to update"
  echo $PULL
  exit 0
fi

kill -9 $(ps ax | grep dotnet | grep urls | awk -F ' ' '{ print $1}')

/root/ChessMeters/
git pull

# database
cd /root/ChessMeters/ChessMeters.Core/
/root/.dotnet/tools/dotnet-ef database update

# npm install
cd /root/ChessMeters/ChessMeters.Web/ClientApp/
npm install

# run
cd /root/ChessMeters/ChessMeters.Web/
# dotnet run --urls "https://www.claudiu.chessmeters.com/" > /dev/null 2>&1 &
dotnet run --urls "https://localhost:5000" > /dev/null 2>&1 &
