using Emgu.CV.Face;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace RCAMREC
{
    public partial class Form1 : Form
    {

        enum RecordingType
        {
            training = 0,
            recognition = 1
        }

        RecordingType recordingType;

        // tamaño de la muestra de la cara
        int modelWith = 100;
        int modelHeigth = 100;
        // Ruta donde se guardara las caras
        string pathSavedFaces = $"{Application.StartupPath}\\Faces\\";
        // Ruta donde se guardara el modelo post entrenado (YAML)
        string pathTrainedFaceModel = $"{Application.StartupPath}\\Faces\\stateModel.yaml";
        // ruta de el modelo pre entrenado para reconocimiento de rostros
        string pathReconozierFaceModel = $"{Application.StartupPath}/haarcascade_frontalface_default.xml";
        // objeto para video grabar la camara
        VideoCapture capture;
        // objeto para aplicar la metodologia haarcascade y reconocer la cara
        CascadeClassifier cascadeClassifier;
        //pbjeto para guardar en temporal las caras
        List<Image<Gray, byte>> trainedImages = new List<Image<Gray, byte>>();
        // Algoritmo de entrenamiento y reconocimiento
        EigenFaceRecognizer eigenFaceRecognizer;
        // Indexamos el rostro
        int faceId = 1;
        string faceNamed = "";
        //Detectamos si es una nueva cara
        bool isAmewFace = false;
        // Eigen
        // numero en la cual la imagen sera dividida  analizada
        int EigenFaceRecognizerComponents = 80;
        // margen de error
        int threshold = 5000;

        public Form1()
        {
            InitializeComponent();
            // inyectamos el modelo de caras a nuestro objeto que lo va a interpretar
            cascadeClassifier = new CascadeClassifier(pathReconozierFaceModel);
            TurnOffCamera();
            FillComboBox();
            faceId = GetNextFaceId();
            ResetInitValues();
        }

        private void ResetInitValues()
        {
            isAmewFace = true;
        }

        private bool isThereAFacesName()
        {
            return textBox1.Text.Trim().Length > 0;
        }

        private bool isThereASelectedFaceId()
        {
            return isAmewFace && isThereAFacesName() || !isAmewFace;
        }

        private int GetNextFaceId()
        {
            int faceId = 0;
            var paths = GetAllFacesPath();
            foreach (var path in paths)
            {
                int cId = int.Parse(GetFaceIdFromAPath(path));
                if (cId > faceId)
                {
                    faceId = cId;
                }

            }
            return Math.Max(faceId, 1) + 1;
        }

        // Vamos a distinguir todos los nombres de las caras guardadas
        private KeyValuePair<string, int> GetItemListFace(string path)
        {
            var slices = path.Split("\\");
            var nameAndIndex = slices[slices.Length - 1].Replace(".bmp", "");
            return new KeyValuePair<string, int>(nameAndIndex.Split("_")[1], int.Parse(nameAndIndex.Split("_")[0]));
        }

        private void FillComboBox()
        {
            var list = GetFacesList();
            comboBox1.Items.Clear();
            comboBox1.Items.Add(new ComboBoxItem("select one", -1));
            List<ComboBoxItem> comboBoxItems = list.Select(v => new ComboBoxItem(v.Key, v.Value)).ToList();
            foreach (var i in comboBoxItems)
                comboBox1.Items.Add(i);
            comboBox1.SelectedValue = -1;
        }

        // LLenamos el combobox con los nombres de los rostros existentes
        private List<KeyValuePair<string, int>> GetFacesList()
        {
            var list = new List<KeyValuePair<string, int>>();
            var allFaces = GetAllFacesPath();
            foreach (var face in allFaces)
            {
                var item = GetItemListFace(face);
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
        private void SoptFace()
        {
            // Empezamos a capturar imagenes por cada frame guardado por la camara en un marco con las medidas de nuestro componente image
            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>().
                Resize(captureImgBox.Size.Width, captureImgBox.Size.Height, Emgu.CV.CvEnum.Inter.Cubic))
            {
                // si capturamos la imagen
                if (imageFrame != null)
                {
                    // convertimos la imagen en gris el archivo de entrenamiento solo trabaja con imagenes en gris y blanco
                    var grayFrame = imageFrame.Convert<Gray, byte>();

                    var faces = cascadeClassifier.DetectMultiScale(grayFrame, 1.4, 4,
                        new Size(imageFrame.Width / 8, imageFrame.Height / 8));
                    // por cada imagen del rostro
                    foreach (var face in faces)
                    {
                        // sacamos una copia a la imagen y la guardamos en una imagen diferente al de la camara
                        var currentFace = imageFrame.Copy(face).Convert<Gray, byte>().Resize(modelWith, modelHeigth, Emgu.CV.CvEnum.Inter.Cubic);
                        // guardamos la copia en una lista temporal de rostros
                        trainedImages.Add(currentFace);
                    }

                    // muestra de imagen principal no copia y pintamos el rectangulo con un grosor de 3 px
                    if (faces.Any())
                    {
                        imageFrame.Draw(faces[0], new Bgr(Color.BurlyWood), 3);
                    }
                }
                // La imagen tomada porla camara la asignaremos a nuestro componente imagen
                captureImgBox.Image = imageFrame;
            }
        }

        private void captureImgBox_Click(object sender, EventArgs e)
        {

        }

        private void TurnOnCamera()
        {
            capture = new VideoCapture();
            this.timer1.Enabled = true;
        }

        private void TurnOffCamera()
        {
            this.timer1.Enabled = false;
            if (captureImgBox.Image != null)
            {
                captureImgBox.Image.Dispose();
                captureImgBox.Image = null;
            }
            if (capture != null)
            {
                capture.Stop();
                capture.Dispose();
            }
        }

        // Obtener todas las imagenes de los rostros
        private string[] GetAllFacesPath()
        {
            return Directory.GetFiles(pathSavedFaces, "*.bmp");
        }

        // Obtener el Id del rostro existente
        private string GetFaceIdFromAPath(string path)
        {
            var slices = path.Split("\\");
            var nameAndIndex = slices[slices.Length - 1].Replace(".bmp", "");
            return nameAndIndex.Split("_")[0];
        }

        // Obtener el siguiente indice de la secuencia
        private string GetAIndexFaceFromPath(string path)
        {
            var slices = path.Split("\\");
            var nameAndIndex = slices[slices.Length - 1].Replace(".bmp", "");
            if (faceId == int.Parse(nameAndIndex.Split("_")[0]))
            {
                return nameAndIndex.Split("_")[1];
            }
            return "";
        }

        private void SaveFaces(string FacesName)
        {
            if (trainedImages.Any() && !string.IsNullOrEmpty(FacesName))
            {
                FacesName = FacesName.Replace("_", "");

                int indx = GetNextIndexFace();
                foreach (var face in trainedImages)
                {
                    face.Save($"{pathSavedFaces}/{faceId}.{FacesName}.{indx}.bmp");
                    indx++;
                }
            }
        }

        // Recupoeramos el indice del primer rostro si ya lo guardamos si no se lo guarda con el indice 1
        private int NextIndexFromAnExistingFace()
        {
            int index = -1;
            var allFaces = GetAllFacesPath();
            foreach (var p in allFaces)
            {
                var faceIndex = GetAIndexFaceFromPath(p);
                if (!string.IsNullOrEmpty(faceIndex))
                {
                    if (int.Parse(faceIndex) > index)
                    {
                        index = int.Parse(faceIndex);
                    }
                }
            }
            return index + 1;
        }

        int GetNextIndexFace()
        {
            if (!isAmewFace)
            {
                return NextIndexFromAnExistingFace();
            }
            else
            {
                return 1;
            }
        }

        // Metodo para entrenar rostros
        private void TrainingFace()
        {
            if (File.Exists(pathTrainedFaceModel))
                File.Delete(pathTrainedFaceModel);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (recordingType == RecordingType.training)
                SoptFace();
            if (recordingType == RecordingType.recognition)
                RecognizeFace();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isThereASelectedFaceId())
            {
                if (isAmewFace)
                {
                    faceNamed = textBox1.Text.Trim();
                }
                recordingType = RecordingType.training;
                TurnOnCamera();
            }
            else
            {
                MessageBox.Show("Introduceo selected a face");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TurnOffCamera();
            // guardamos las imagenes cuando la camara deje de grabar
            SaveFaces(faceNamed);

            FillComboBox();
        }

        private bool TrainDataSetWithEigenFaceRecognizer()
        {
            if (File.Exists(pathTrainedFaceModel))
            {
                File.Delete(pathTrainedFaceModel);

                var faces = GetAllFacesPath();
                if (faces != null && faces.Length > 0)
                {
                    EigenFaceRecognizer eigenFaceRecognizer = new EigenFaceRecognizer(EigenFaceRecognizerComponents, threshold);
                    var images = new Image<Gray, byte>[faces.Length];
                    var labels = new int[images.Length];

                    for (int i = 0; i < faces.Length; i++)
                    {
                        var faceImage = new Image<Gray, byte>(faces[i]).Resize(modelWith, modelHeigth, Emgu.CV.CvEnum.Inter.Cubic);
                        images[i] = faceImage;
                        labels[i] = int.Parse(GetFaceIdFromAPath(faces[i]));
                    }

                    VectorOfMat vecmat = new VectorOfMat();
                    VectorOfInt veclabels = new VectorOfInt();

                    vecmat.Push(images);
                    veclabels.Push(labels);
                    eigenFaceRecognizer.Train(vecmat, veclabels);
                    eigenFaceRecognizer.Write(pathTrainedFaceModel);
                    return true;
                }
            }
            return false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (item != null)
            {
                int val = item.Value;
                isAmewFace = !(val > 0);
                if (isAmewFace)
                {
                    faceId = GetNextFaceId();
                    faceNamed = textBox1.Text.Trim();
                }
                else
                {
                    faceId = val;
                    textBox1.Text = "";
                    faceNamed = item.Text;
                }
            }
        }

        string GetFaceName(int label)
        {
            return "Jorge";
        }

        void RecognizeFace()
        {
            string name = "unknown";
            using(var imageFrame = capture.QueryFrame().ToImage<Bgr, Byte>().Resize(captureImgBox.Size.Width, captureImgBox.Size.Height, Emgu.CV.CvEnum.Inter.Cubic))
            {
                if (imageFrame != null) 
                {
                    var grayFrame = imageFrame.Convert<Gray, byte>();
                    var faces = cascadeClassifier.DetectMultiScale(grayFrame, 1.4, 4,
                        new Size(imageFrame.Width / 8, imageFrame.Height / 8));

                    foreach (var face in faces)
                    {
                        var imageToCompare = imageFrame.Copy(face).Convert<Gray, byte>().Resize(modelWith, modelHeigth, Emgu.CV.CvEnum.Inter.Cubic);
                        var result = eigenFaceRecognizer.Predict(imageToCompare);

                        if (result.Label > 0)
                        {
                            name = GetFaceName(result.Label);
                        }
                    }

                    if (faces.Any())
                    {
                        imageFrame.Draw(faces[0], new Bgr(Color.BurlyWood), 3);
                    }
                    current_detected.Text = name;
                    captureImgBox.Image = imageFrame;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool wastrained = TrainDataSetWithEigenFaceRecognizer();
            if (wastrained)
            {
                MessageBox.Show("Entrenando");
            }
            else
            {
                MessageBox.Show("Fallo");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            capture = new VideoCapture();
            recordingType = RecordingType.recognition;

            if (File.Exists(pathTrainedFaceModel))
            {
                eigenFaceRecognizer.Read(pathTrainedFaceModel);
                TurnOnCamera();
            }
            else
            {
                MessageBox.Show("No esta entrenado el modelo de reconocmiento");
            }

        }
    }
}
