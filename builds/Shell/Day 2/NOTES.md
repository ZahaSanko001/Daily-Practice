# Day 2

## The Path environment variable

--- 

Every process has a block of environment variables as NAME=value text pairs. The OS or the parent process (in this case the shell) hands these variables to the program when it starts. `PATH` is one of them, it holds a list of executable files. 

Linux/macOS:  /usr/local/bin:/usr/bin:/bin
Windows:      C:\Windows\System32;C:\Program Files\dotnet

directories are separated by `:` or `;` based on the OS. The `Path.PathSeparator` basically provides us with the separator based on the current OS.

The shell ha an Environment Variable block, set up by the config files like .bashrc
When we run `dotnet run` the shell launches the program as a child process and passes on a copy of the Environment Variable block.
.NET reads that block into memory. The `Environment.GetEnvironmentVariable("PATH)` basically just looks up the string for the key "PATH" from the Environment Variable block. We need to check if it's null as well.

In our programm we get the `pathEnv` as the whole string, `"/usr/bin:/bin"`.
We need to split them using the `Path.PathSeparator` - `string[] indPaths = pathEnv.Split(Path.PathSeparator)`.
This gives us `["/usr/bin", "/bin"]`
These are only directories not the path to any executables. 
That's why we combine our command with the directory `Path.Combine(dir, name)`, and then check if this path exists and if it is an executable `File.Exists(fullPath) && IsExecutable(fullPath)`

This give us conformation that the executable exists and the actual path of the executable. 