#!/bin/bash

git pull

# database
cd /root/ChessMeters/ChessMeters.Core/
/root/.dotnet/tools/dotnet-ef database update

# npm install
cd /root/ChessMeters/ChessMeters.Web/ClientApp
npm install
