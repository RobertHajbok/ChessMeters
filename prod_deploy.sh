#!/bin/bash

function print_dotnet_count() {
  ps ax | grep 'dotnet run --urls' > ~/tmp
  CNT=`cat ~/tmp | grep -v "grep" | wc -l`
  echo "Found $CNT dotnet processes."
}

function stop_dotnet() {
  echo "Searching for dotnet processes..."
  print_dotnet_count
  
  echo "Shutting down dotnet run..."
  sudo kill $(ps ax | grep 'dotnet run --urls' | grep -v 'grep' | awk -F ' ' '{ print $1 }')
  sleep 5
  
  print_dotnet_count
  rm ~/tmp
}

echo ""
echo "Redeploying PROD..."
echo ""

stop_dotnet
git pull

# echo "Updating DB..."
# cd ChessMeters.Core
# sudo ~/.dotnet/tools/dotnet-ef database update
# cd ..

echo "Starting dotnet..."
cd ChessMeters.Web
sudo lsof -t -i tcp:80 -s tcp:listen | sudo xargs kill
sudo dotnet run --urls "http://claudiu.chessmeters.com/" >/dev/null 2>&1 &
echo "Redeploy DONE."
