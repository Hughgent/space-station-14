﻿#nullable enable
using System;
using System.Collections.Generic;
using Content.Shared.Chemistry;
using Robust.Shared.GameObjects;
using Robust.Shared.GameObjects.Components.UserInterface;
using Robust.Shared.Serialization;

namespace Content.Shared.GameObjects.Components.Chemistry
{

    /// <summary>
    /// Shared class for <c>ChemMasterComponent</c>. Provides a way for entities to split reagents from a beaker and produce pills and bottles via a user interface.
    /// </summary>
    public class SharedChemMasterComponent : Component
    {
        public override string Name => "ChemMaster";

        [Serializable, NetSerializable]
        public class ChemMasterBoundUserInterfaceState : BoundUserInterfaceState
        {
            public readonly bool HasPower;
            public readonly bool HasBeaker;
            public readonly ReagentUnit BeakerCurrentVolume;
            public readonly ReagentUnit BeakerMaxVolume;
            public readonly string ContainerName;

            /// <summary>
            /// A list of the reagents and their amounts within the beaker/reagent container, if applicable.
            /// </summary>
            public readonly List<Solution.ReagentQuantity> ContainerReagents;
            /// <summary>
            /// A list of the reagents and their amounts within the buffer, if applicable.
            /// </summary>
            public readonly List<Solution.ReagentQuantity> BufferReagents;
            public readonly string DispenserName;

            public readonly bool BufferModeTransfer;

            public readonly ReagentUnit BufferCurrentVolume;
            public readonly ReagentUnit BufferMaxVolume;

            public ChemMasterBoundUserInterfaceState(bool hasPower, bool hasBeaker, ReagentUnit beakerCurrentVolume, ReagentUnit beakerMaxVolume, string containerName,
                string dispenserName, List<Solution.ReagentQuantity> containerReagents, List<Solution.ReagentQuantity> bufferReagents, bool bufferModeTransfer, ReagentUnit bufferCurrentVolume, ReagentUnit bufferMaxVolume)
            {
                HasPower = hasPower;
                HasBeaker = hasBeaker;
                BeakerCurrentVolume = beakerCurrentVolume;
                BeakerMaxVolume = beakerMaxVolume;
                ContainerName = containerName;
                DispenserName = dispenserName;
                ContainerReagents = containerReagents;
                BufferReagents = bufferReagents;
                BufferModeTransfer = bufferModeTransfer;
                BufferCurrentVolume = bufferCurrentVolume;
                BufferMaxVolume = bufferMaxVolume;
            }
        }

        /// <summary>
        /// Message data sent from client to server when a ChemMaster ui button is pressed.
        /// </summary>
        [Serializable, NetSerializable]
        public class UiActionMessage : BoundUserInterfaceMessage
        {
            public readonly UiAction action;
            public readonly ReagentUnit amount;
            public readonly string id = "";
            public readonly bool isBuffer;
            public readonly int pillAmount;
            public readonly int bottleAmount;

            public UiActionMessage(UiAction _action, ReagentUnit? _amount, string? _id, bool? _isBuffer, int? _pillAmount, int? _bottleAmount)
            {
                action = _action;
                if (action == UiAction.ChemButton)
                {
                    amount = _amount.GetValueOrDefault();
                    if (_id == null)
                    {
                        id = "null";
                    }
                    else
                    {
                        id = _id;
                    }

                    isBuffer = _isBuffer.GetValueOrDefault();
                }
                else
                {
                    pillAmount = _pillAmount.GetValueOrDefault();
                    bottleAmount = _bottleAmount.GetValueOrDefault();
                }
            }
        }

        [Serializable, NetSerializable]
        public enum ChemMasterUiKey
        {
            Key
        }

        /// <summary>
        /// Used in <see cref="UiButtonPressedMessage"/> to specify which button was pressed.
        /// </summary>
        public enum UiAction
        {
            Eject,
            Transfer,
            Discard,
            ChemButton,
            CreatePills,
            CreateBottles
        }

    }
}
