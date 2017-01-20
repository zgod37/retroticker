## Overview

RetroTicker is a simple scrolling text ticker written in C# to display and animate messages.

What's "retro" about the RetroTicker?  Similar to traditional-style electronic tickers that use a grid of lightbulbs to display their messages, RetroTicker is comprised of a grid of Panels that displays messages by changing the background colors for each Panel.  This simple implementation gives the ticker a retro feel to it, and also allowed for me to design my own font and custom animations.

Here's an example:

<iframe src='https://gfycat.com/ifr/ConstantAdorableBaboon' frameborder='0' scrolling='no' width='640' height='32' allowfullscreen></iframe>

### Features

#### Read messages from a file

Custom messages can be added, 1 per line, in the "messages.txt" file located in the data folder.

#### Read messages from an irc channel

RetroTicker can also be used as a "chat reader" to display messages from a Twitch.tv stream chat.  For this feature you will need a valid Twitch account and Twitch oauth password, which can be obtained [here](https://twitchapps.com/tmi/).  Enter your Twitch credentials by selecting "config" from the File menu on the main window.  Click the "connect" button to connect your bot to chat.

NOTE: During times of high chat activity the ticker will only display a portion of the messages received to prevent the chat reader from lagging too far behind.

#### Display messages

To display messages, click the "open ticker" button and then click the "start reading" button.  If your twitch bot is connected, it will display messages from chat, otherwise it will read from "messages.txt".  The ticker will continuously display messages until the "stop reading" button is clicked.