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

**TIERS START AT 0 NOT 1.**

| Variable | Default value |
| ------------- | ------------- |
|`b079_enable` | `false`
|`b079_a1_dist`| `15.0`
|`b079_a1_power` | `15.0`
|`b079_a1_tier` | `0`
|`b079_a2_power` | `75.0`
|`b079_a2_tier` | `2`
|`b079_a2_timer` | `5`
|`b079_a2_gas_timer` | `10`
|`b079_a2_exp` | `35.0`
|`b079_a2_scpdmg` | `false`
|`b079_a2_blacklisted_rooms` | `[]`
|`b079_a3_power` | `100.0`
|`b079_a3_tier` | `1`
|`b079_a3_timer`  | `30.0`
|`b079_a4_power` | `40.0`
|`b079_a4_tier` | `1`
|`b079_prefix` | `079`
|`b079_help_title` | `Abilities/Commands:`
|`b079_help_a1` | `Teleport to your SCP friends`
|`b079_help_a2` | `Activate a memetic in the current room (only on humans)`
|`b079_help_a3` | `Shutdown Zone Lighting`
|`b079_help_a4` | `Camera Flash (blinds others)`
|`b079_msg_tier_required` | `Tier $tier+ Required`
|`b079_msg_no_power` | `Not enough power.`
|`b079_msg_help_cmd_fail` | `Invalid. Type ".$prefix ?" for help.`
|`b079_msg_a1_fail` | `No SCPs found.`
|`b079_msg_a1_run` | `Teleporting...`
|`b079_msg_a2_fail` | `Memetics don't work here!`
|`b079_msg_a2_run` | `Activating...`
|`b079_msg_a3_run` | `Overcharging...`
|`b079_msg_a4_run` | `Flashing...`
|`b079_msg_a2_warn` | `<color=#ff0000>MEMETIC KILL AGENT will activate in this room in $seconds seconds.</color>`
|`b079_msg_a2_active` | `<color=#ff0000>MEMETIC KILL AGENT ACTIVATED.</color>`
|`b079_spawn_msg` | `<color=#00ff00>[Better079] Type \".079 help\" in the console for more abilities.</color>`
