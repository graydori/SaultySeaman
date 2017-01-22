Notes for the next jam:

Next time I want to try Unity Collaborate

We spent a bunch of time on Shan's machine trying to get UnitySmartMerge working smoothly with Git

https://docs.unity3d.com/Manual/SmartMerge.html 
https://gist.github.com/Ikalou/197c414d62f45a1193fd 
http://kihira.uk/unity-and-git-getting-them-to-play-nicely/ 

in the end we couldn't even tell if it was working or not because it didn't produce any output when "git merge" called
it. afterwards, git merge still said we had a scene merge conflict.
we only knew git merge was even calling it because initially git merge complained about the path to it not being valid.

in the end Shan merged by eyeballing who changed what in the Github app, then using Notepad
to edit the diffs, and running "git merge" on the command line.
