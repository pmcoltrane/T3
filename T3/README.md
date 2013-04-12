# T3 - A wrapper for Okuma's T3 library

## Overview

This is a simple library for accessing status data from legacy Okuma CNCs through the T3 library option. 

I cannot find the documentation for the underlying Okuma library. It is developed based on the library text output for a couple of OSP-P200 lathe simulators. I've excluded the Mmsnt-t3.dll library provided by Okuma, because I'm not sure about redistribution.

I have no access to older CNCs, so I don't know how it will behave on a real machine.

## Requirements

* An Okuma CNC with the T3 option installed.
* The Mmsnt-t3.dll library provided by Okuma.
* .NET framework

## Notes

### Known Issues

The CNC status data is incorrect. The indicator bits (alarm, limit, program stop, slide hold, STM, and running) do not properly reflect the current machine state.

I have heard reports of T3 library queries on E-100 or 7000 series CNCs triggering a D-level alarm. This seems to occur when requesting MacMan data, not status, but again, I do not have access to a proper CNC for testing. 

### Usage

```vbnet

Dim machine As New T3.Machine("10.1.2.3")
machine.Connect()

Dim status as T3.Status = machine.GetStatus()
machine.Disconnect()

MessageBox.Show("Machine mode is: " & status.Mode.Mode.ToString())

```