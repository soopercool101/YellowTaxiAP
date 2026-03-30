using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Packets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Archipelago.MultiClient.Net.Converters;
using Archipelago.MultiClient.Net.Exceptions;

namespace YellowTaxiAP.Archipelago
{
    public class RingLinkHandler
    {/*
        public static bool RingLinkEnabled;

        readonly IArchipelagoSocketHelper socket;
        readonly IConnectionInfoProvider connectionInfoProvider;

        internal RingLink lastSendRingLink;

        /// <summary>
        /// Creates <see cref="OnRingLinkReceived"/> event for clients to hook into and decide what to do with the
        /// received <see cref="RingLink"/>
        /// </summary>
        public delegate void RingLinkReceivedHandler(RingLink RingLink);
        /// <summary>
        /// Delegate event that supplies the created <see cref="RingLink"/> whenever one is received from the server
        /// as a bounce packet.
        /// </summary>
        public event RingLinkReceivedHandler OnRingLinkReceived;

        internal RingLinkHandler(IArchipelagoSocketHelper socket, IConnectionInfoProvider connectionInfoProvider)
        {
            this.socket = socket;
            this.connectionInfoProvider = connectionInfoProvider;

            socket.PacketReceived += OnPacketReceived;
        }

        void OnPacketReceived(ArchipelagoPacketBase packet)
        {
            switch (packet)
            {
                case BouncedPacket bouncedPacket when bouncedPacket.Tags.Contains("RingLink"):
                    if (RingLink.TryParse(bouncedPacket.Data, out var RingLinkValue))
                    {
                        if (lastSendRingLink != null && lastSendRingLink == RingLinkValue)
                            return;

                        if (OnRingLinkReceived != null)
                            OnRingLinkReceived(RingLinkValue);
                    }
                    break;
            }
        }

        // ReSharper disable once UnusedMember.Global
        /// <summary>
        ///     Formats and sends a Bounce packet using the provided <paramref name="RingLink"/> object.
        /// </summary>
        /// <param name="RingLink">
        ///     <see cref="RingLink"/> object containing the information of the Ring which occurred.
        ///     Must at least contain the <see cref="RingLink.Source"/>.
        /// </param>
        /// <exception cref="T:ArchipelagoSocketClosedException">
        ///     The websocket connection is not alive
        /// </exception>
        public void SendRingLink(RingLink RingLink)
        {
            var bouncePacket = new BouncePacket
            {
                Tags = new List<string> { "RingLink" },
                Data = new Dictionary<string, JToken> {
                    {"time", RingLink.Timestamp.ToUnixTimeStamp()},
                    {"source", RingLink.Source},
                }
            };

            if (RingLink.Cause != null)
                bouncePacket.Data.Add("cause", RingLink.Cause);

            lastSendRingLink = RingLink;

            socket.SendPacketAsync(bouncePacket);
        }
    }

    /// <summary>
    /// A RingLink object that gets sent and received via bounce packets.
    /// </summary>
    public class RingLink : IEquatable<RingLink>
    {
        /// <summary>
        /// The Timestamp of the created RingLink object
        /// </summary>
        public DateTime Timestamp { get; internal set; }
        /// <summary>
        /// The name of the player who sent the RingLink
        /// </summary>
        public long Source { get; }
        /// <summary>
        /// The full text to print for players receiving the RingLink. Can be null
        /// </summary>
        public long Amount { get; }

        /// <summary>
        /// A RingLink object that gets sent and received via bounce packets.
        /// </summary>
        /// <param name="sourcePlayer">Name of the player sending the RingLink</param>
        /// <param name="cause">Optional reason for the RingLink. Since this is optional it should generally include
        /// a name as if this entire text is what will be displayed</param>
        public RingLink(long sourceId, long amount)
        {
            Timestamp = DateTime.UtcNow;
            Source = sourceId;
            Amount = amount;
        }

        internal static bool TryParse(Dictionary<string, JToken> data, out RingLink RingLink)
        {
            try
            {
                if (!data.TryGetValue("time", out JToken timeStampToken) || !data.TryGetValue("source", out JToken sourceToken))
                {
                    RingLink = null;
                    return false;
                }

                string cause = null;
                if (data.TryGetValue("cause", out JToken causeToken))
                {
                    cause = causeToken.ToString();
                }

                RingLink = new RingLink(sourceToken.ToString(), cause)
                {
                    Timestamp = UnixTimeConverter.UnixTimeStampToDateTime(timeStampToken.ToObject<double>()),
                };
                return true;
            }
            catch
            {
                RingLink = null;
                return false;
            }
        }

        /// <inheritdoc/>
        public bool Equals(RingLink other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Source == other.Source
                   && Timestamp.Date.Equals(other.Timestamp.Date)
                   && Timestamp.Hour == other.Timestamp.Hour
                   && Timestamp.Minute == other.Timestamp.Minute
                   && Timestamp.Second == other.Timestamp.Second;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((RingLink)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable once NonReadonlyMemberInGetHashCode
                var hashCode = Timestamp.GetHashCode();
                hashCode = (hashCode * 397) ^ (Source != null ? Source.GetHashCode() : 0);
                return hashCode;
            }
        }

#pragma warning disable CS1591
        public static bool operator ==(RingLink lhs, RingLink rhs) => lhs?.Equals(rhs) ?? rhs is null;

        public static bool operator !=(RingLink lhs, RingLink rhs) => !(lhs == rhs);
#pragma warning restore CS1591
    */}
}
