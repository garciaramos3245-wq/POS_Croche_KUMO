[CmdletBinding()]
param(
    [string]$Servidor = '.\SQLEXPRESS',
    [string]$ArchivoSql
)

$ErrorActionPreference = 'Stop'

function New-SqlConnection([string]$BaseDatos) {
    $cadena = "Server=$Servidor;Database=$BaseDatos;Integrated Security=True;Connect Timeout=8;Application Name=KUMO Setup"
    return New-Object System.Data.SqlClient.SqlConnection $cadena
}

if ([String]::IsNullOrWhiteSpace($ArchivoSql)) {
    $ArchivoSql = Join-Path -Path $PSScriptRoot -ChildPath 'KUMOBD.sql'
}

if (-not (Test-Path -LiteralPath $ArchivoSql)) {
    throw "No se encontro el archivo SQL: $ArchivoSql"
}

Add-Type -AssemblyName System.Data

$rutaSql = (Resolve-Path -LiteralPath $ArchivoSql).Path
$utf8 = New-Object System.Text.UTF8Encoding($false)
$contenido = [System.IO.File]::ReadAllText($rutaSql, $utf8)
$lotes = [regex]::Split($contenido, '(?im)^\s*GO\s*(?:--.*)?$') |
    Where-Object { -not [String]::IsNullOrWhiteSpace($_) }

$conexion = New-SqlConnection 'master'

try {
    Write-Host "Conectando a SQL Server Express en $Servidor ..."
    $conexion.Open()

    foreach ($lote in $lotes) {
        $comando = $conexion.CreateCommand()
        $comando.CommandText = $lote
        $comando.CommandTimeout = 120
        [void]$comando.ExecuteNonQuery()
        $comando.Dispose()
    }
}
catch {
    Write-Error ("No se pudo configurar KUMOBD en {0}. {1}" -f $Servidor, $_.Exception.Message)
    exit 1
}
finally {
    $conexion.Dispose()
}

$verificacion = New-SqlConnection 'KUMOBD'

try {
    $verificacion.Open()
    $comando = $verificacion.CreateCommand()
    $comando.CommandText = 'SELECT COUNT(*) FROM dbo.PRODUCTO'
    $productos = [int]$comando.ExecuteScalar()
    $comando.Dispose()

    Write-Host ""
    Write-Host "KUMOBD esta lista para KUMO POS." -ForegroundColor Green
    Write-Host "Servidor: $Servidor"
    Write-Host "Productos disponibles: $productos"
    Write-Host "Ya puedes abrir WindowsApplication2.exe."
}
finally {
    $verificacion.Dispose()
}
