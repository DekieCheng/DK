param($installPath, $toolsPath, $package, $project)

# change deployed artifact to copy-to-bin
$project.ProjectItems.Item("Plugin.PNALib.Windows.config").Properties.Item("CopyToOutputDirectory").Value = 1
