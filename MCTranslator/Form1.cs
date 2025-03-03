using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
namespace MCTranslator
{
    public partial class Form1 : Form
    {
        private string selectedFolder = string.Empty;
        public Form1()
        {
            InitializeComponent();
            button2.Click += SelectFolder; 
            button1.Click += StartTranslationAsync; 
            button3.Click += ExcludeMod; 
            button4.Click += RestoreMod;
            button1.Enabled = false; 
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void SelectFolder(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @".minecraft\mods");
                if (Directory.Exists(defaultPath))
                {
                    dialog.SelectedPath = defaultPath;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolder = dialog.SelectedPath;
                    var jarFiles = Directory.GetFiles(selectedFolder, "*.jar"); 

                    listBox1.Items.Clear();
                    listBox2.Items.Clear(); 
                    listBox1.Items.AddRange(jarFiles.Select(Path.GetFileName).ToArray());

                    var itemsToCheck = listBox1.Items.Cast<string>().ToList();
                    var removedItemExist = false;
                    foreach (var item in itemsToCheck)
                    {
                        string jarFile = Path.Combine(selectedFolder, item);

                        if (FindKorJson(jarFile) || !FindEnJson(jarFile))
                        {
                            removedItemExist = true;
                            listBox1.Items.Remove(item); 
                            listBox2.Items.Add(item); 
                        }
                    }

                    label4.Text = $"{listBox1.Items.Count}��";
                    label5.Text = $"{listBox2.Items.Count}��";

                    button1.Enabled = listBox1.Items.Count > 0;
                    button3.Enabled = jarFiles.Length > 0;
                    button4.Enabled = removedItemExist;
                    progressBar1.Visible = false;
                    if (listBox1.Items.Count == 0)
                    {
                        MessageBox.Show("������ �ʿ����� �ʽ��ϴ�.");
                    }
                }
            }
        }

        private async void StartTranslationAsync(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0)
            {
                MessageBox.Show("API Ű�� �����ϴ�. GPT APIŰ�� �Է��� �ּ���.");
                return;
            }
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("������ ��尡 �����ϴ�!\n��� ��尡 �̹� �ѱ�ȭ �Ǿ� �ְų� ������ �Ұ����� �����Դϴ�.");
                return;
            }
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            progressBar1.Visible = true;

            richTextBox1.Clear();
            AppendTextWithScroll("������ �����մϴ�...\n");

            var itemsToCheck = listBox1.Items.Cast<string>().ToList();

            progressBar1.Maximum = listBox1.Items.Count;
            progressBar1.Value = 0;

            int index = 1;
            int totalItems = itemsToCheck.Count;

            foreach (var item in itemsToCheck)
            {
                string jarFile = Path.Combine(selectedFolder, item.ToString());
                string jsonContent = ExtractLangFile(jarFile);

                if (jsonContent != null)
                {
                    AppendTextWithScroll($"$\"[{{index}}/{{totalItems}}][{item}] ��� Ȯ�� ��...\n");
                    string translatedJson = await TranslateLargeJson(jsonContent, textBox1.Text, item.ToString());
                    AppendTextWithScroll($"��� ���� Ȯ�� �Ϸ�.. ���� ����\n");
                    if (translatedJson != null)
                    {
                        SaveTranslatedJsonToJar(jarFile, translatedJson);
                        AppendTextWithScroll($"[{item}] ���� �Ϸ�.\n");
                    }
                    else
                    {
                        AppendTextWithScroll($"[{item}] ���� ����!\n");
                    }
                }
                else
                {
                    AppendTextWithScroll($"[{item}] lang/en_us.json ����\n");
                    listBox2.Items.Add(item);
                    listBox1.Items.Remove(item);
                }
                progressBar1.Value++;
                index++;
            }

            AppendTextWithScroll("��� ������ �������ϴ� ����� �κ��� �ִٸ� .jar������ ���� �����ϰ� ���.jar\assets\\����̸�\\lang\\ko_kr.json���� ������ �ּ���!\n");
            MessageBox.Show("��� ���� �۾��� �Ϸ�Ǿ����ϴ�!\n ������:MintSoda07", "���� �Ϸ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            progressBar1.Visible = false;

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }
        private void ExcludeMod(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var selectedMod = listBox1.SelectedItem.ToString();
                listBox2.Items.Add(selectedMod); 
                listBox1.Items.Remove(selectedMod); 

                label4.Text = $"{listBox1.Items.Count}��";
                label5.Text = $"{listBox2.Items.Count}��";
                if(listBox1.Items.Count <= 0)
                {
                    button1.Enabled = false;
                    button3.Enabled = false;
                }
                button4.Enabled = true;
            }
        }

        private void RestoreMod(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                var selectedMod = listBox2.SelectedItem.ToString();
                listBox1.Items.Add(selectedMod); 
                listBox2.Items.Remove(selectedMod); 

                label4.Text = $"{listBox1.Items.Count}��";
                label5.Text = $"{listBox2.Items.Count}��";
                if (listBox1.Items.Count >= 1)
                {
                    button1.Enabled = true;
                    button3.Enabled = true;
                }
                if(listBox2.Items.Count <= 0)
                {
                    button4.Enabled = false;
                }
            }
        }
        private string ExtractLangFile(string jarFile)
        {
            using (ZipArchive archive = ZipFile.OpenRead(jarFile))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.Contains("lang/en_us.json"))
                    {
                        using (StreamReader reader = new StreamReader(entry.Open()))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }
        private void AppendTextWithScroll(string text)
        {
            richTextBox1.AppendText(text);
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        private async Task<string> TranslateJson(string jsonContent, string apiKey)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(2); 
                    var requestBody = new
                    {
                        model = "gpt-4o",
                        max_tokens = 8192,
                        messages = new[]
                        {
                            new { role = "system", content = "Translate the following JSON file from English to Korean. Keep the JSON structure identical." },
                            new { role = "user", content = jsonContent }
                        }
                    };

                    string jsonRequest = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        dynamic responseData = JsonConvert.DeserializeObject(responseString);
                        return responseData.choices[0].message.content;
                    }
                    else
                    {
                        AppendTextWithScroll($"GPT API ����: {response.ReasonPhrase}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                AppendTextWithScroll($"���� ��û �� ���� �߻�: {ex.Message}\n");
            }
            return null;
        }
        private bool FindKorJson(string jarFile)
        {
            using (ZipArchive archive = ZipFile.OpenRead(jarFile))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.Contains("assets/") && entry.FullName.Contains("/lang/") && entry.FullName.EndsWith("ko_kr.json"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool FindEnJson(string jarFile)
        {
            using (ZipArchive archive = ZipFile.OpenRead(jarFile))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.Contains("assets/") && entry.FullName.Contains("/lang/") && entry.FullName.EndsWith("en_us.json"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private List<Dictionary<string, string>> SplitJson(string jsonContent, int chunkSize = 50)
        {
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
            var chunks = new List<Dictionary<string, string>>();
            Dictionary<string, string> currentChunk = new Dictionary<string, string>();

            foreach (var pair in jsonData)
            {
                if (currentChunk.Count >= chunkSize)
                {
                    chunks.Add(new Dictionary<string, string>(currentChunk));
                    currentChunk.Clear();
                }
                currentChunk[pair.Key] = pair.Value;
            }

            if (currentChunk.Count > 0)
            {
                chunks.Add(currentChunk);
            }

            return chunks;
        }
        private async Task<Dictionary<string, string>> TranslateChunk(Dictionary<string, string> chunk, string apiKey)
        {
            string jsonChunk = JsonConvert.SerializeObject(chunk);
            string translatedJson = await TranslateJson(jsonChunk, apiKey);
            try
            {
                translatedJson = translatedJson.Replace("`", "");
            }
            catch(Exception ex)
            {

            }
            
            if (translatedJson.StartsWith("json"))
            {
                translatedJson = translatedJson.Substring(4).Trim(); 
            }

            File.WriteAllText("debug_gpt_response.txt", translatedJson);
            try
            {
                var translatedDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(translatedJson);

                richTextBox1.AppendText($"[����] JSON ��ȯ �Ϸ�: {translatedDict.Count}���� �׸� ������.\n");
                return translatedDict;
            }
            catch (JsonReaderException ex)
            {
                richTextBox1.AppendText($"[����] JSON ��ȯ ����! GPT ������ Ȯ���ϼ���.\n{ex.Message}\nGPT API�� ���� �ѵ��� �ʰ��Ͽ��ų� ������ �������� �ʾ��� �� �ֽ��ϴ�.");
                MessageBox.Show($"JSON ��ȯ �� ������ �߻��Ͽ����ϴ�. \n{ex.Message}\n ��Ȯ�� ���� �α׸� bin/debug_gpt_response.txt���� Ȯ���ϼ���.");

                return null;
            }
        }
        private async Task<string> TranslateLargeJson(string jsonContent, string apiKey,string modName)
        {
            List<Dictionary<string, string>> chunks = SplitJson(jsonContent);
            Dictionary<string, string> translatedData = new Dictionary<string, string>();

            for (int i = 0; i < chunks.Count; i++)
            {
                AppendTextWithScroll($"[{modName}]{i}��° ���� ��ū ���� ��...\n");
                var translatedChunk = await TranslateChunk(chunks[i], apiKey);

                if (translatedChunk != null)
                {
                    foreach (var entry in translatedChunk)
                    {
                        translatedData[entry.Key] = entry.Value;
                    }
                }

                AppendTextWithScroll($"[{modName}] ���� �Ϸ�.\n");
            }

            return JsonConvert.SerializeObject(translatedData, Formatting.Indented);
        }
        private void SaveTranslatedJsonToJar(string jarPath, string translatedJson)
        {
            try
            {
                string tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(tempDir);
                ZipFile.ExtractToDirectory(jarPath, tempDir);

                // assets ���� ���ο� �ִ� "lang" ���� ã��
                string assetsFolderPath = Path.Combine(tempDir, "assets");
                string langFolderPath = FindLangFolder(assetsFolderPath); // ��庰 ���� �ڵ� Ž��

                if (langFolderPath == null)
                {
                    AppendTextWithScroll($"lang ������ ã�� �� �����ϴ�: {jarPath}\n");
                    return;
                }

                string koKrFilePath = Path.Combine(langFolderPath, "ko_kr.json");
                File.WriteAllText(koKrFilePath, translatedJson, Encoding.UTF8);

                string tempJarPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(jarPath));
                if (File.Exists(tempJarPath)) File.Delete(tempJarPath);

                ZipFile.CreateFromDirectory(tempDir, tempJarPath);
                Directory.Delete(tempDir, true);
                File.Copy(tempJarPath, jarPath, true);

                AppendTextWithScroll($"ko_kr.json ���� �Ϸ�: {jarPath}\n");
            }
            catch (Exception ex)
            {
                AppendTextWithScroll($"ko_kr.json ���� �� ���� �߻�: {ex.Message}\n");
            }
        }
        private string FindLangFolder(string assetsFolderPath)
        {
            if (!Directory.Exists(assetsFolderPath)) return null;

            foreach (string folder in Directory.GetDirectories(assetsFolderPath))
            {
                string langPath = Path.Combine(folder, "lang");
                if (Directory.Exists(langPath))
                {
                    return langPath; // assets/*�������*/lang ��� ��ȯ
                }
            }

            return null; // ã�� ���� ���
        }
    }
}
