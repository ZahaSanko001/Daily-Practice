# Building a Shell from first principles

```
Implemented so far --->
```

# REPL (Read-Eval-Print Loop)
simple enough, just put the prompt inside a INF while loop
# Builtins
For most part it seems trying to read a command by slicing(command[..4]) is an issue, the command.StartsWith() method handles the cases where the command[..4] gives error for shorter commands.
Implemented so far --- exit, echo, type


# Notes
Day 1: a lot if if statements. Maybe figure out a switch statement or compress the ifs.

# Final
Will commit here the final build