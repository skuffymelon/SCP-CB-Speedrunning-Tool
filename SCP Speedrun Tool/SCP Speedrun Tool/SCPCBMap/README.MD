# Seed Previewer

Launch app with following command:
`java -jar scpcbmapServer.jar`

Server will be available at port 1499 (unless other port is specified in `server.properties`).

Index page: http://localhost:1499/frontend/

### Seed finder

SeedFinder is bruteforce script that will generate every possible seed and analyze routes using various pathfinders.
Script supports following argline parameters:
- `--start` - seed number to start from (inclusive);
- `--end` - seed number to stop (exclusive);
- `--print-maze` - draw map to console like S-NAV
- `--path-finders` - PathFinder java class that will perform map analysis, and threshold route length, separated by colon.
  Example value: `fully.qualified.Classname:123`.
  If you want multiple pathfinders, separate them by comma.

Example command:  
`java -cp scpcbmap.jar ru.sooslick.scpcb.SeedFinder --print-maze --start=10000 --end=20000 --path-finders=ru.sooslick.scpcb.pathfinder.SSPathFinder:42,ru.sooslick.scpcb.pathfinder.SSIPathFinder:66`  
This command will generate seeds from 10000 to 19999, printing each found seed.

Existing Pathfinder classes and recommended scores:
- `ru.sooslick.scpcb.pathfinder.SSPathFinder:30` - Set Seed Inbounds A1/B1 with PD route;
- `ru.sooslick.scpcb.pathfinder.SSLegacyPathFinder:45` - Set Seed Inbounds A1/B1 with classic route;
- `ru.sooslick.scpcb.pathfinder.SSA2PathFinder:35` - Set Seed Inbounds A2 with PD route;
- `ru.sooslick.scpcb.pathfinder.SSB2PathFinder:35` - Set Seed Inbounds B2 with PD route;
- `ru.sooslick.scpcb.pathfinder.SSIPathFinder:50` - Set Seed Intended A1/B1;
- `ru.sooslick.scpcb.pathfinder.SSIA2PathFinder:52` - Set Seed Intended A2;
- `ru.sooslick.scpcb.pathfinder.SSIB2PathFinder:55` - Set Seed Intended B2;
- `ru.sooslick.scpcb.pathfinder.AnyPercentPathFinder:30` - Any% route (Out of Bounds)

Other experimental pathfinders also may be available in `ru.sooslick.scpcb.pathfinder` package