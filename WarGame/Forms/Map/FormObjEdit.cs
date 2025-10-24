namespace WarGame.Forms.Map;

public partial class FormObjEdit : Form
{
    private readonly StaticObject? _obj;

    public FormObjEdit()
    {
        InitializeComponent();
        _obj = FormMap.ObjectsStatic.Items.Find(x => x.Selected);
        if (_obj == null)
        {
            Close();
            return;
        }

        textBoxName.Select();

        Text = $"[{_obj.Id:0}] {_obj.Name}";
        textBoxId.Text = $"{_obj.Id:0}";
        textBoxName.Text = $"{_obj.Name:0}";

        button2.Click += Button2_Click;
    }

    private async void Button2_Click(object? sender, EventArgs e)
    {
        if (_obj == null)
        {
            Close();
            return;
        }

        _obj.Name = textBoxName.Text;
        var ret = await FormMap.ObjectsStatic.ChangeAsync();
        if (!ret)
        {
            MessageBox.Show("Сохранение не удалось!", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        Close();
    }
}
