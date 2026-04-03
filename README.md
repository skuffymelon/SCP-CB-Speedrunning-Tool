# SCP-CB Speedrunning Tool

### A program for SCP Containment Breach speedrunners and casual players aswell!

Features:
  - **Bridge between the game and the tool (allows for tracking of achievements and cheats)** *(only speedrun mod is supported right now.)*
  - Maynard <-> Maintenence code conversion.
  - Map layout generator with labels.
  - Seed generator for ranked speedrunning.

## SYSTEM REQUIREMENTS

- **30 MB** of free storage.
- Uses **50 MB** of RAM.
- With ranked seeds cached in RAM, uses **130 MB** max.

## Map Layout uses Sooslick's "scpcbUtility" tool to generate seeds.
His software is in the files for **local map generation** which is way faster than downloading data from the web.

Because it uses java, **you must have java installed for local map generation to work**.

- https://www.oracle.com/java/technologies/javase/javase8-archive-downloads.html

The link to Sooslick's software is listed down below.

- https://github.com/Sooslick/scpcbUtility/tree/master

## SOURCE CODE SETUP AND BUILD
  - **This program was made in the .NET 8.0 framework.**
    
  - Download the source code.
  - Open the solution file.
  - Build to make/revert assembly references (nuget cache)
  - You're all set!

# CHANGELOG
### 4/3/2026
## (0.3a) - THE BRIDGE UPDATE
- Added bridge between the tool and SCP Containment Breach (Speedrun Mod)
- Achievement Tracking Overlay
- Maynard <-> Maintenence Code Converter Overlay
- Cheats accessible through new SCP - CB Menu
- Cheats also accessible with hotkeys (Check help menu in the tool for help on hotkeys)
- Fixed a few oversights and bugs from the last version.
- Added "Enable Cheats" option in the Settings.
### 3/9/2026
## (0.2a)
- Fixed program crashing upon closure.
(Trying to close java server)
- Tidied up files, which are now placed in asset directories.
- Added program icon.
### 3/6/2026
## First alpha release of SCP-CB ST: (0.1a)
- Maynard <-> Maintenence code conversion.
- Map layout generator.
- Ranked seed generator.
