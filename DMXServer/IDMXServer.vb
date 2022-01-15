''' <summary>
''' This interface defines methods to communicate with a dmx server.
''' </summary>
''' <remarks></remarks>
Public Interface IDMXServer
    Delegate Sub ValueChangedCallback(ByVal Channel As Integer, ByVal value As Integer)
    ''' <summary>
    ''' Sets the number of used channels. This may improve the performance of the bus
    ''' </summary>
    ''' <param name="count">The number of the channels used</param>
    ''' <remarks>Should only be set once at startup.</remarks>
    Sub SetChannelCount(ByVal count As Integer)

    ''' <summary>
    ''' Sets the value of a dmx channel.
    ''' </summary>
    ''' <param name="channel">The channel to set</param>
    ''' <param name="value">The value to write</param>
    ''' <remarks></remarks>
    Sub SetData(ByVal channel As Integer, ByVal value As Integer)

    ''' <summary>
    ''' Starts an observer for the value of the given channel
    ''' </summary>
    ''' <param name="channel">The channel to observe</param>
    ''' <param name="callback">The callback that is executed upon a change of the observed channel</param>
    ''' <remarks></remarks>
    Sub AddObserver(ByVal channel As Integer, ByVal callback As ValueChangedCallback)

    ''' <summary>
    ''' Removes an observer for the value of the given channel
    ''' </summary>
    ''' <param name="channel">The channel that was observed</param>
    ''' <param name="callback">The callback that was executed upon a change of the observed channel</param>
    ''' <remarks></remarks>
    Sub RemoveObserver(ByVal channel As Integer, ByVal callback As ValueChangedCallback)

    ''' <summary>
    ''' Get the value for a channel
    ''' </summary>
    ''' <param name="channel">The channel to get the value for</param>
    ''' <returns>The value of the requested channel or -1 if the channel does not exist.</returns>
    ''' <remarks></remarks>
    Function GetData(ByVal channel As Integer) As Integer
End Interface
