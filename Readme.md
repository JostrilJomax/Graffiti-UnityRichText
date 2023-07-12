# Graffiti - Rich Text for Unity using Html Tags

#### It's a small project, made mostly for fun

This projects allows you to modify string using html tags and<br/>
modifier characters like Underline, Strikethrough.<br/>

It also supports gradients and multiple styles on the same string.<br/>
There is an option to specify the range of modification for each style.<br/>

So, for example, one style can colorize first half of a string and the second<br/>
one can add underline to the rest of a string.<br/>

All html colors are of short hex format (#RGB)

### Basics

<details>
  <summary>Examples, Code and Description</summary>

![](Documentation~/Basic_1.png)
![](Documentation~/Basic_1_1.png)

</details>

### Range specification

<details>
  <summary>Examples, Code and Description</summary>

![](Documentation~/Basic_2.png)
![](Documentation~/Basic_2_1.png)

</details>

### Available settings

<details>
  <summary>Settings</summary>

![](Documentation~/Graffiti_4.png)

</details>

### Other Examples

<details>
  <summary>Other</summary>

![](Documentation~/Graffiti_2.png)
![](Documentation~/Graffiti_3.png)

</details>

### How to download

Create new Unity project and clone this repository to <b>Assets/Plugins/</b> folder<br/>
To see what it can do go to <b>Assets/Plugins/Graffiti/Resources</b> folder<br/>
And select ScriptableObject <b>"Graffiti Settings"</b>. It contains all examples tests.<br/>
There are several test scripts in <b>Graffiti/Scripts/Tests</b> that

Be warned! This project was not tested and it contains huge amount of bugs, but it works.

### Summary

There is not much use to it in it's current state.<br/>
It's not optimized, it can't perform complex selection operations,<br/>
so you need to specify modification range using IndexRange or by index of a word.<br/>
It also has some bugs when modifier characters with gradient overlap.<br/>

I wanted to utilize Unity console html support to it's limits and I think I succeeded.
