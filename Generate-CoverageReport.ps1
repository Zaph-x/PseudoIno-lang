dotnet tool install --global minicover
dotnet restore
dotnet build

# Instrument
minicover instrument --sources .\**\*.cs --exclude-sources .\Tests\**\*.cs --tests .\Tests\**\*.cs

# Reset hits
minicover reset

dotnet test --no-build

# Uninstrument
minicover uninstrument

# Create html reports inside folder coverage-html
minicover htmlreport --threshold 90

# Console report
minicover report --threshold 90