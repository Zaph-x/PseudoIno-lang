#!/bin/bash

passCount=$(ls -1q ./premade-programs/pass | wc -l)
failCount=$(ls -1q ./premade-programs/fail | wc -l)
exePath="./Core/bin/Debug/netcoreapp3.1/"
pathBack="../../../.."

function ExpectError() {
    if [ $1 -eq 0 ]
    then
        tput setaf 1;echo "[file: $2.pi ($exitcode)] Compiler compiled when it should not. Exiting."
        exit -1
    fi
}

function ExpectNoError() {
    exitcode=$1
    if [ $1 -ne 0 ]
    then
        tput setaf 1;echo "[file: $2.pi ($exitcode)] Compiler dit not compile when it should. Exiting."
        exit -1
    elif [ $1 -ge 124 ]; then
        tput setaf 1;echo "Compiler exited with code $exitcode... (http://www.gnu.org/software/coreutils/manual/html_node/timeout-invocation.html)"
        exit $1
    fi
}

cd $exePath
./Core

tput setaf 2;echo "Checking passing compilations!"
tput setaf 9
for ((i=1;i<=$passCount;i++))
do
    $(timeout --preserve-status 4s ./Core "$pathBack/premade-programs/pass/$i.pi")
    ExpectNoError $? $i
done

tput setaf 2;echo "Checking failing compilations!"
tput setaf 9
for ((i=1;i<=$failCount;i++))
do
    $(timeout --preserve-status 4s ./Core "$pathBack/premade-programs/fail/$i.pi")
    ExpectError $? $i
done
cd $pathBack
tput setaf 2;echo "All tests passed successfully!"
exit 0