dotnet tool install minicover
dotnet restore
dotnet build
# Instrument
dotnet minicover instrument --sources "./**/*.cs" --exclude-sources "./Tests/**/*.cs" --tests "./Tests/**/*.cs"

# Reset hits
dotnet minicover reset
dotnet test --no-build

# Uninstrument
dotnet minicover uninstrument
# Create html reports inside folder coverage-html
dotnet minicover htmlreport --threshold 80

# Console report
dotnet minicover report --threshold 80