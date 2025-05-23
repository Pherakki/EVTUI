namespace EVTUI.ViewModels.TimelineCommands;

public class FAB_ : Generic
{
    public FAB_(DataManager config, SerialCommand command, object commandData) : base(config, command, commandData)
    {
        this.LongName = "Field: Base Animation";
        this.AssetID = new IntSelectionField("Asset ID", this.Editable, this.Command.ObjectId, config.EventManager.AssetIDsOfType(0x00000003));

        this.ObjectIndex = new NumEntryField("Field Object Index", this.Editable, this.CommandData.ObjectIndex, 0, 65535, 1);
        this.FirstAnimation = new AnimationWidget(config, this.AssetID, this.CommandData.FirstAnimation, this.CommandData.Flags, $"First Animation", frameBlendingInd:1);
        this.SecondAnimation = new AnimationWidget(config, this.AssetID, this.CommandData.SecondAnimation, this.CommandData.Flags, $"Second Animation", enabledInd:0, frameBlendingInd:2, enabledFlip:true);
        this.DebugFrameForward = new BoolChoiceField("Frame Forward", this.Editable, this.CommandData.Flags[31]);

    }

    public IntSelectionField AssetID { get; set; }

    public NumEntryField   ObjectIndex     { get; set; }
    public AnimationWidget FirstAnimation  { get; set; }
    public AnimationWidget SecondAnimation { get; set; }

    public BoolChoiceField DebugFrameForward { get; set; }

    public new void SaveChanges()
    {
        base.SaveChanges();
        this.Command.ObjectId = this.AssetID.Choice;

        this.CommandData.ObjectIndex = (uint)this.ObjectIndex.Value;
        this.FirstAnimation.SaveChanges();
        this.SecondAnimation.SaveChanges();
        this.CommandData.Flags[31] = this.DebugFrameForward.Value;
    }
}
