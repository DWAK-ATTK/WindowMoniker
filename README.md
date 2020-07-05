# WindowMoniker  

## Introduction  
**WindowMoniker** places a customizable frame around your screen.  
Useful when you work with multiple computers, such as Virtual Machines, physical computers, or remote desktop sessions.

## How To Use  
### Installation
* Download a [WindowMoniker release](/releases).  
* Extract the zip file anywhere you like.  I prefer `C:\Program Files\WindowMoniker`.
* Locate and open the `WindowMoniker.exe.config` file.  This is an XML file that can be opened in Notepad or your favorite text editor.
	* Adjust the configuration values to your liking.
	* Save and close the config file.
* Locate and open `WindowMoniker.exe`.  
* Enjoy the shiny chrome.

### Make it run at startup  
<kbd>Win+R</kbd> to open the Run dialog.  Type: `shell:startup` and press <kbd>Enter</kbd>  
Then create a shortcut to WindowMoniker in the startup folder you just opened.

### Shut it down?
Hold <kbd>Shift</kbd> and **right-click** the border.  You should see an **Exit** menu appear.  

## Support
Make sure you scroll down and read the Liability section.  
I offer no support what-so-ever.  You may not contact me regarding this project.  If you do manage to figure out how to contact me,
all such contact will go unanswered.

You **may** file *bug reports* and *feature requests* on the GitHub [issue tracker](/issues).   
Any issue that is not a legitimate bug report or feature request will go unanswered and be deleted. 

## Liability  
This is a personal project.  It is being made available AS-IS to anyone who would like to use it.
I am in no way responsible for any consequences arising directly or indirectly from the download, distribution, or use of this software.
I make no claims as to the completeness, reliability, correctness, viability, taste, touch, or smell of this software.
USE AT YOUR OWN RISK.

## What's it look like?
![WindowMoniker 1.1](/.github/WM-1.1-SS01.png)  

### Customization
In addition to the stylish pink and white motif depicted above, you may customize several aspects of the program:
* **ForeColor**  (pink in image)
* **BackColor**  (white in image)
* **Title**      (text in the blue box at the top of the image)
* **TitleForeColor**  (yellow in image)
* **TitleBackColor**  (blue box behind the text in the image)
* **BorderMode** (Solid, LeftDiagonal, RightDiagonal, Dashed, Dotted.  Dashed in image)
* **Edges**      (All, Top, Bottom, Left, Right  (comma separated).  All in image)  


Not all setting in the configuration file are supported in the 1.1 release.