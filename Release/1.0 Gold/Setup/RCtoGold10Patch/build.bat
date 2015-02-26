@echo off
REM Paso 0. Preparar entorno de ejecución (reemplazar los directorios por los que se correspondan con la realidad)
echo 0. Setting environment variables
set PatchPrjPath=C:\Users\Developer\Documents\Visual Studio 2012\Projects\Codeplex\MovistarTV\Release\1.0 Gold\Setup\RCtoGold10Patch
set PatchSrcPath=C:\Users\Developer\Downloads
path %path%;C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1A\Bin
path %path%;C:\Program Files (x86)\WiX Toolset v3.9\bin

REM Paso 1: Crear directorio temporal para descomprimir los MSI de la RC y la Gold
echo 1a. Cleaning build environment
cd %PatchPrjPath%
rmdir source /s /q
rmdir obj /s /q
rmdir bin /s /q
echo 1b. Creating build directories
md source
md source\RC
md source\Gold
md obj
md bin

REM 2. Realizar instalación administrativa de la versión RC
echo 2. Decompressing RC version files
start /WAIT msiexec /a "%PatchSrcPath%\DVB-IPTV_MovistarTV_1-0-RC.msi" /qb TARGETDIR="%PatchPrjPath%\source\RC"

REM 3. Realizar instalación administrativa de la versión Gold
echo 3. Decompressing GOLD version files
start /WAIT msiexec /a "%PatchSrcPath%\DVB-IPTV_MovistarTV_1-0_Gold.msi" /qb TARGETDIR="%PatchPrjPath%\source\Gold"

REM 4. Build the Patch (WiX portion)
echo 4. Building the patch (WiX portion)
candle RCtoGold10PCP.wxs -out .\obj\
light.exe .\obj\RCtoGold10PCP.wixobj -out .\obj\RCtoGold10PCP.pcp

REM 5. Build the Patch (MSI portion)
echo 5. Building the patch (MSI SDK portion)
msimsp.exe -s .\obj\RCtoGold10PCP.pcp -p .\bin\DVB-IPTV_MovistarTV_1-0_RC-to-Gold.msp -l .\obj\RCtoGold10PCP.msi.log.txt

echo MSP patch file build is complete!
