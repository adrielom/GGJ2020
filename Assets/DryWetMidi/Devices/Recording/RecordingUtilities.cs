﻿using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
    /// <summary>
    /// Contains methods to manipulate by recording of MIDI data.
    /// </summary>
    public static class RecordingUtilities
    {
        #region Methods

        /// <summary>
        /// Creates an instance of the <see cref="TrackChunk"/> and places recorded events to it.
        /// </summary>
        /// <param name="recording"><see cref="Recording"/> to place events to <see cref="TrackChunk"/> from.</param>
        /// <returns><see cref="TrackChunk"/> with events recorded with <see cref="Recording"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="recording"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="recording"/> is in progress.</exception>
        public static TrackChunk ToTrackChunk(this Recording recording)
        {
            ThrowIfArgument.IsNull(nameof(recording), recording);

            if (recording.IsRunning)
                throw new ArgumentException("Recording is in progress.", nameof(recording));

            return recording.GetEvents().ToTrackChunk();
        }

        /// <summary>
        /// Creates an instance of the <see cref="MidiFile"/> and places recorded events to it.
        /// </summary>
        /// <param name="recording"><see cref="Recording"/> to place events to <see cref="MidiFile"/> from.</param>
        /// <returns><see cref="MidiFile"/> with events recorded with <see cref="Recording"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="recording"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="recording"/> is in progress.</exception>
        public static MidiFile ToFile(this Recording recording)
        {
            ThrowIfArgument.IsNull(nameof(recording), recording);

            if (recording.IsRunning)
                throw new ArgumentException("Recording is in progress.", nameof(recording));

            var trackChunk = recording.ToTrackChunk();

            var midiFile = new MidiFile(trackChunk);
            midiFile.ReplaceTempoMap(recording.TempoMap);
            return midiFile;
        }

        #endregion
    }
}
