using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;

namespace WpfApp.VigenereCipher
{
  public partial class MainWindow : Window
  {
    const string RussianAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    const string EnglishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string Key;
    string InputText;
    string OutputText;
    public MainWindow()
    {
      InitializeComponent();
      MessageBox.Show("В открытом тексте и ключе учитываются только буквы,\n" +
        "содержащиеся в алфавите выбранного языка.\n" );
    }
    private void buttonBrowse_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog()
      {
        CheckFileExists = false,
        CheckPathExists = true,
        Multiselect = false,
        Title = "Choose file",
        Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
      };

      if (dialog.ShowDialog() == true)
      {
        tbInput.Text = File.ReadAllText(dialog.FileName, Encoding.UTF8).ToUpper();
      }

    }
    private void buttonSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog()
      {
        Title = "Save file",
        Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
      };
      if (dialog.ShowDialog() == true)
      {
        File.WriteAllText(dialog.FileName, tbOutput.Text, Encoding.UTF8);
      }
    }
    private void GetKey(string Alphabet)
    {
      string InputKey = "";
      for (int i = 0; i < tbKey.Text.Length; i++)
      {
        if (Alphabet.Contains(tbKey.Text[i].ToString()))
          InputKey += tbKey.Text[i].ToString();
      }
      if (InputKey.Length != InputText.Length)
      {
        int i = 0;
        while (Key.Length != InputText.Length)
        {
          Key += InputKey[i];
          i++;
          if (i == InputKey.Length) i = 0;
        }
      }
      else
      {
        Key = InputKey;
      }
    }
    private void GetInputText(string Alphabet)
    {
      for (int i = 0; i < tbInput.Text.Length; i++)
      {
        if (Alphabet.Contains(tbInput.Text[i].ToString()))
          InputText += tbInput.Text[i].ToString();
      }
    }
    private void buttonEncrypt_Click(object sender, RoutedEventArgs e)
    {
      InputText = "";
      OutputText = "";
      Key = "";

      if (radioEnglish.IsChecked == true)
      {
        GetInputText(EnglishAlphabet);
        GetKey(EnglishAlphabet);
        Encryption(EnglishAlphabet);
      }
      if (radioRussian.IsChecked == true)
      {
        GetInputText(RussianAlphabet);
        GetKey(RussianAlphabet);
        Encryption(RussianAlphabet);
      }
    }
    private void Encryption(string Alphabet)
    {
      for (int i = 0; i < InputText.Length; i++)
      {
        int Pi = Alphabet.IndexOf(InputText[i]);
        int Ki = Alphabet.IndexOf(Key[i]);
        int Ci = (Pi + Ki) % Alphabet.Length;
        OutputText += Alphabet[Ci];
      }
      tbOutput.Text = OutputText;
    }
    private void buttonDecrypt_Click(object sender, RoutedEventArgs e)
    {
      InputText = "";
      OutputText = "";
      Key = "";

      if (radioEnglish.IsChecked == true)
      {
        GetInputText(EnglishAlphabet);
        GetKey(EnglishAlphabet);
        Decryption(EnglishAlphabet);
      }
      if (radioRussian.IsChecked == true)
      {
        GetInputText(RussianAlphabet);
        GetKey(RussianAlphabet);
        Decryption(RussianAlphabet);
      }
    }
    private void Decryption(string Alphabet)
    {
      for (int i = 0; i < InputText.Length; i++)
      {
        int Ci = Alphabet.IndexOf(InputText[i]);
        int Ki = Alphabet.IndexOf(Key[i]);
        int Pi = (Ci - Ki + Alphabet.Length) % Alphabet.Length;
        OutputText += Alphabet[Pi];
      }
      tbOutput.Text = OutputText;
    }
  }
}
