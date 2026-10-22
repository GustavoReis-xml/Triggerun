@echo off
chcp 65001 >nul
title Reiniciar Servidor Triggerun (Nuvem Oracle)
color 0B
cls
echo ================================================================
echo    [REINICIAR] SERVIDOR TRIGGERUN NA NUVEM (ORACLE VPS)
echo ================================================================
echo.
echo Conectando ao servidor e reiniciando o servico PM2...
echo.

ssh -i "C:\Users\Gustavo\Downloads\ssh-key-2026-09-04.key" -o StrictHostKeyChecking=no ubuntu@136.248.105.85 "sudo pm2 restart triggerun && echo && sudo pm2 status"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ================================================================
    echo    [SUCESSO] SERVIDOR REINICIADO COM SUCESSO!
    echo ================================================================
) else (
    echo.
    echo ================================================================
    echo    [ERRO] FALHA AO REINICIAR! Verifique sua conexao de internet.
    echo ================================================================
)

echo.
echo Esta janela fechara automaticamente em 4 segundos...
ping -n 5 127.0.0.1 >nul
