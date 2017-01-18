## RetroTicker

RetroTicker is a simple scrolling text ticker for Windows to display and animate messages with a "retro feel" to it.

What's so "retro" about the RetroTicker?  Similar to traditional-style tickers that use a grid of lightbulbs to display their messages (think NYSE), RetroTicker is comprised of a grid of Panels that displays messages by changing the background colors for each Panel.  By implementing the ticker in this way, I was able to create my own font and custom retro-style animations.

Here's an example:

<iframe src='https://gfycat.com/ifr/ConstantAdorableBaboon' frameborder='0' scrolling='no' width='640' height='32' allowfullscreen></iframe>

This project was purely meant to be a learning exercise to gain experience with the MVC design pattern.  Sure, there are plenty of great libraries and programs that come with pre-built graphics and animations, but I wanted to come up with a my own version, while also creating a low-level implementation that could be potentially used with other interfaces. Plus I've always liked the look of retro-style animations, and this was a fun way to explore that art style!

### Features

RetroTicker can read from a custom list of messages provided by the user, or it can be used as a "chat reader" to read from an Twitch.tv irc channel.

#### Read messages from a file

To add your own custom messages to display on the ticker, locate the "messages.txt" file in the data folder and open it with your favorite text editing software.  Here you can add and edit your custom messages as you please, in the format of one message per line.  Once you've saved your messages, close/re-open RetroTicker and click the "start reading" button to have the ticker display your messages. The ticker will loop your messages infinitely until the "stop reading" button is clicked.

#### Read messages from an irc channel

RetroTicker can also be used as a "chat reader" to display messages from a twitch stream chat.  For this feature you will need a valid Twitch account and Twitch oauth password, which can be obtained [here](https://twitchapps.com/tmi/).  

On the main RetroTicker form, go to File > config and enter your twitch credentials, including the channel you would like to read from (e.g. if its your own channel, simply enter #*your_twitch_nick*).  Then click the connect button to connect and join the channel.  If your bot successfully connects, click the "start reading" button to have the chat messages displayed on the ticker.  The ticker will continue reading until the "stop reading" button is clicked or it disconnects from the server.

NOTE: In chats with high message rates, the ticker will only display a portion of the messages received, to prevent the chat reader from lagging behind too far.  The amount of messages displayed is proportional to the message rate of the channel, i.e. how many messages are received in a given time frame.
