namespace WarGame.Forms.Map;

public partial class FormObjAntennaEdit : Form
{
    private readonly StaticObjAntenna? _obj;
    private int _angle;
    private int _width;
    private int _lenKm;
    private bool _save = false;

    public FormObjAntennaEdit()
    {
        InitializeComponent();

        _obj = FormMap.ObjectsStatic.Items.Find(x => x.Selected) as StaticObjAntenna;
        if (_obj == null)
        {
            Close();
            return;
        }

        textBoxName.Select();

        Text = $"[{_obj.Id:0}] {_obj.Name}";
        textBoxId.Text = $"{_obj.Id:0}";
        textBoxName.Text = $"{_obj.Name:0}";

        _angle = (int)(_obj.Parameters.Angle * 180.0f / Math.PI);
        textBoxAngle.Text = $"{_angle:0}";
        trackBarAngle.Value = _angle;

        _width = (int)(_obj.Parameters.Width * 180.0f / Math.PI);
        textBoxWidth.Text = $"{_width:0}";
        trackBarWidth.Value = _width;

        _lenKm = (int)(_obj.Parameters.LenKm);
        textBoxLenKm.Text = $"{_lenKm:0}";
        trackBarLenKm.Value = _lenKm;

        checkBoxDrawRadio.Checked = _obj.Parameters.DrawRadio;

        button2.Click += Button2_Click;
        trackBarAngle.ValueChanged += TrackBarAngle_ValueChanged;
        trackBarLenKm.ValueChanged += TrackBarLenKm_ValueChanged;
        trackBarWidth.ValueChanged += TrackBarWidth_ValueChanged;
        checkBoxDrawRadio.CheckedChanged += CheckBoxDrawRadio_CheckedChanged;
        Closing += FormObjAntennaEdit_Closing;
    }

    private async void FormObjAntennaEdit_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (_save)
        {
            var ret = await FormMap.ObjectsStatic.ChangeAsync();
            if (!ret)
            {
                MessageBox.Show("Сохранение не удалось!", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }
        await FormMap.ObjectsStatic.UpdateAsync();
    }

    private void CheckBoxDrawRadio_CheckedChanged(object? sender, EventArgs e)
    {
        if (_obj == null) return;
        _obj.Parameters.DrawRadio = !_obj.Parameters.DrawRadio;
    }

    private void TrackBarWidth_ValueChanged(object? sender, EventArgs e)
    {
        if (_obj == null) return;
        _obj.Parameters.Width = (float)(trackBarWidth.Value * (Math.PI / 180.0f));

        _width = (int)(_obj.Parameters.Width * 180.0f / Math.PI);
        textBoxWidth.Text = $"{_width:0}";
    }

    private void TrackBarLenKm_ValueChanged(object? sender, EventArgs e)
    {
        if (_obj == null) return;
        _obj.Parameters.LenKm = (float)(trackBarLenKm.Value);
        _lenKm = (int)(_obj.Parameters.LenKm);
        textBoxLenKm.Text = $"{_lenKm:0}";
    }

    private void TrackBarAngle_ValueChanged(object? sender, EventArgs e)
    {
        if (_obj == null) return;
        _obj.Parameters.Angle = (float)(trackBarAngle.Value * (Math.PI / 180.0f));
        _angle = (int)(_obj.Parameters.Angle * 180.0f / Math.PI);
        textBoxAngle.Text = $"{_angle:0}";
    }

    private void Button2_Click(object? sender, EventArgs e)
    {
        if (_obj == null)
        {
            Close();
            return;
        }

        _obj.Name = textBoxName.Text;
        _save = true;
        Close();
    }
}
