# Import ClearCase to Git

This tool is based on the excellent work of [lanfeust69](https://github.com/lanfeust69) with the [clearcase-to-git-importer](https://github.com/lanfeust69/clearcase-to-git) and is able to import a ClearCase VOB to a Git repository (or several different repositories). 

It has been tested on a ClearCase VOB with 17 years of history resulting in a Git repository with a size of ~500MB. The import time was about 2-3 days. It has also been tested on a much smaller VOB that imported in some hours.

## Major differences in this fork from lanfeust69

- Without doubt lower code base quality since I'm unfamiliar with C# and I didn't bother to clean up the code (sorry).
- Supports extracting subfolders in the VOB to different Git repositories.
- Will drop useless or empty commits and labels.
- Significantly more aggressive with merging multiple ClearCase checkins to a Git commit.
- More aggressive with trying to keep the lables correct at a slight cost of the correctness of the history.
- Imports the author and date of a label.
- If lables are not matched properly then the Git tag will be annotated with a message.
- Better support for some edge cases, though there are still many unsoved one remaining.
- Partial support for convering charset to UTF-8 in metadata. Filenames are known to not be converted properly.
- Support for incremental imports has been dropped. It's all or nothing!
- Renaming of files is ignored and instead it leaves everything to Git to figure out.

It has only been tested with a dynamic ClearCase view. Support for thirdparties has not been tested.

## General principle

- Export as much as possible using `clearexport_ccase` (in several parts due to memory constraints of `clearexport_ccase`).
- Get all elements (files and directories).
- Optionally edit these lists to exclude uninteresting ones.
- Use `GitImporter` (which calls `cleartool`) to create (and save) a representation of the VOB.
- Import with `GitImporter` and `git fast-import`. `cleartool` is then used only to get the content of files. This is done repeatedly if importing to multiple Git repositoriesm, though not more than needed.

## Compiling

If you're lucky you might be able to compile with this command:

```
C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /debug /define:DEBUG /define:TRACE /r:protobuf-net.dll /out:scripts\GitImporter.exe /pdb:scripts\GitImporter *.cs
```

## Usage

It must be run with the Git Bash in Windows.

Look inside the scripts folder for an example `import.sh`. Also make sure to modify the `gitignore` file there as it will be checked in.

The provided `import.sh` will take a VOB and create several Git repositories from it, assuming all folders in the VOB are equivalent to different Git repositories.

For example, the following ClearCase VOB...

```
my_vob
├── somefolder/
|   └── file.txt
└── anotherfolder/
    └── file2.txt
```

...will be transformed to...

```
somefolder/
├── .git/
└── file.txt
anotherfolder/
├── .git/
└── file2.txt
```

When ready run `import.sh`. The Git repositories will be created in a folder named `git-import`.
