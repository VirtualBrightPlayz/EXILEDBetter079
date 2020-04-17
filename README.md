# EXILEDBetter079

## Commands

Just type `.079 ?` into the GAME console (`~`) when you are SCP-079 to show a list of abilities.

## Abilities

### A1

Teleport to an SCP.

### A2

Activate a memetic kill agent in a room. Takes `b079_a2_timer` seconds to charge and lasts for `b079_a2_gas_timer` seconds.

### A3

Turn off facility lighting for a specific amount of time (`b079_a3_timer` seconds).

### A4

Flash all who are looking at the camera with a flashbang.

## Configs

TIERS START AT 0 NOT 1!!!

`b079_enable` - default `false`


`b079_a1_dist` - default `15.0`

`b079_a1_power` - default `15.0`

`b079_a1_tier` - default `0`


`b079_a2_power` - default `75.0`

`b079_a2_tier` - default `2`

`b079_a2_timer` - default `5`

`b079_a2_gas_timer` - default `10`

`b079_a2_exp` - default `35.0`

`b079_a2_blacklisted_rooms` - default `[]`


`b079_a3_power` - default `100.0`

`b079_a3_tier` - default `1`

`b079_a3_timer`  - default `30.0`


`b079_a4_power` - default `40.0`

`b079_a4_tier` - default `1`


`b079_prefix` - default `079`

`b079_help_title` - default `Abilities/Commands:`

`b079_help_a1` - default `Teleport to your SCP friends`

`b079_help_a2` - default `Activate a memetic in the current room (only on humans)`

`b079_help_a3` - default `Shutdown Zone Lighting`

`b079_help_a4` - default `Camera Flash (blinds others)`


`b079_msg_tier_required` - default `Tier $tier+ Required`

`b079_msg_no_power` - default `Not enough power.`

`b079_msg_help_cmd_fail` - default `Invalid. Type ".$prefix ?" for help.`


`b079_msg_a1_fail` - default `No SCPs found.`

`b079_msg_a1_run` - default `Teleporting...`


`b079_msg_a2_fail` - default `Memetics don't work here!`

`b079_msg_a2_run` - default `Activating...`


`b079_msg_a3_run` - default `Overcharging...`

`b079_msg_a4_run` - default `Flashing...`


`b079_msg_a2_warn` - default `<color=#ff0000>MEMETIC KILL AGENT will activate in this room in $seconds seconds.</color>`

`b079_msg_a2_active` - default `<color=#ff0000>MEMETIC KILL AGENT ACTIVATED.</color>`


`b079_spawn_msg` - default `<color=#00ff00>[Better079] Type \".079 help\" in the console for more abilities.</color>`