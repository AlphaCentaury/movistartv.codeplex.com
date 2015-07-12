@echo off
REM Paso 0. Preparar entorno de ejecución (reemplazar los directorios por los que se correspondan con la realidad)
echo 0. Setting environment variables
set PatchPrjPath=C:\Users\Developer\Documents\Visual Studio 2012\Projects\Codeplex\MovistarTV\Development\1.0 Update 1 'Wolf 424'\Setup\GoldToUpdate1Patch
set PatchSrcPath=C:\Users\Developer\Documents\Visual Studio 2012\Projects\Codeplex\MovistarTV\Development\1.0 Update 1 'Wolf 424'\Setup\GoldToUpdate1Patch\MSI
path %path%;C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1A\Bin
path %path%;C:\Program Files (x86)\WiX Toolset v3.9\bin

REM Paso 1: Crear directorio temporal para descomprimir los MSI de la Gold y la Update 1
echo 1a. Cleaning build environment
cd %PatchPrjPath%
rmdir source /s /q
rmdir obj /s /q
rmdir bin /s /q
echo 1b. Creating build directories
md source
md source\Gold
md source\Update1
md obj
md bin

REM 2. Realizar instalación administrativa de la versión Gold
echo 2. Decompressing GOLD version files
start /WAIT msiexec /a "%PatchSrcPath%\DVB-IPTV_MovistarTV_1-0_Gold.msi" /qb TARGETDIR="%PatchPrjPath%\source\Gold"

REM 3. Realizar instalación administrativa de la versión Update1
echo 3. Decompressing UPDATE-1 version files
start /WAIT msiexec /a "%PatchSrcPath%\DVB-IPTV_MovistarTV_1-0_Update-1.msi" /qb TARGETDIR="%PatchPrjPath%\source\Update1"

REM 4. Build the Patch (WiX portion)
echo 4. Building the patch (WiX portion)
candle GoldToUpdate1PatchPCP.wxs -out .\obj\
light.exe .\obj\GoldToUpdate1PatchPCP.wixobj -out .\obj\GoldToUpdate1PatchPCP.pcp

REM 5. Build the Patch (MSI portion)
echo 5. Building the patch (MSI SDK portion)
msimsp.exe -s .\obj\GoldToUpdate1PatchPCP.pcp -p .\bin\DVB-IPTV_MovistarTV_1-0_Gold-to-Update-1.msp -l .\obj\GoldToUpdate1PatchPCP.msi.log.txt

echo MSP patch file build is complete!
