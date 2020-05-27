dotnet tool install minicover | out-null
"Building project"
dotnet restore | out-null
dotnet build | out-null
"Setting up minicover"
# Instrument
dotnet minicover instrument --sources .\**\*.cs --exclude-sources .\Tests\**\*.cs --tests .\Tests\**\*.cs | out-null
"Reseting previous tests"
# Reset hits
dotnet minicover reset | out-null
"Running tests"
dotnet test --no-build | out-null

# Uninstrument
dotnet minicover uninstrument | out-null
"Generating HTML report"
# Create html reports inside folder coverage-html
dotnet minicover htmlreport --threshold 80 | out-null

# Console report
dotnet minicover report --threshold 80