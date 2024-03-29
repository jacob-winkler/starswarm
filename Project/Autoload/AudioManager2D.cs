using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using StarSwarm.Project.Autoload.AudioLibraries;
using StarSwarm.Project.Common;

namespace StarSwarm.Project.Autoload
{
    [Tool]
    public class AudioManager2D : Node2D
    {
        public void Play(KnownAudioStream2Ds audioStream, Vector2? position = null)
        {
            var audioPlayer = GetAudioPlayer(audioStream, position);

            audioPlayer.PlayAndDispose();
        }

        public override String _GetConfigurationWarning()
        {
            var warning = String.Empty;
            var enumValues = Enum.GetValues(typeof(KnownAudioStream2Ds)).OfType<object>().Select(x => x.ToString());
            
            foreach (var stream in enumValues)
            {
                var node = GetNode(stream.ToString());
                if (node == null)
                    warning += $"Missing audio node: '{stream}'\n";
            }

            foreach(var child in GetChildren())
            {
                if(!enumValues.Contains(((Node)child).Name))
                    warning += $"Missing KnownAudioStream2Ds enum value: '{((Node)child).Name}'\n";
            }

            return warning.TrimEnd('\n');
        }

        private DisposableAudioStreamPlayer GetAudioPlayer(KnownAudioStream2Ds audioStream, Vector2? position)
        {
            var audioPlayer = GetNode<Node>(audioStream.ToString());

            if (audioPlayer is AudioLibrary library)
            {
                audioPlayer = library.GetAudioPlayer();
            }

            audioPlayer = audioPlayer.Duplicate();
            var disposableAudioPlayer = new DisposableAudioStreamPlayer((AudioStreamPlayer2D)audioPlayer);

            AddChild(disposableAudioPlayer);

            if(position != null)
                disposableAudioPlayer.SetPosition(position.Value);

            return disposableAudioPlayer;
        }
    }
}
