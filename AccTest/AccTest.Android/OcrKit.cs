using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Google.MLKit.Vision.Common;

using Xamarin.Google.MLKit.Vision.Text;
[assembly: Dependency(typeof(AccTest.Droid.OcrKit))]
namespace AccTest.Droid
{
   
    public class OcrKit : IOcrKit
    {
        class TaskCompleteListener : Java.Lang.Object, IOnCompleteListener
        {
            private readonly TaskCompletionSource<Java.Lang.Object> taskCompletionSource;

            public TaskCompleteListener(TaskCompletionSource<Java.Lang.Object> tcs)
            {
                this.taskCompletionSource = tcs;
            }

            public void OnComplete(Android.Gms.Tasks.Task task)
            {
                if (task.IsCanceled)
                {
                    this.taskCompletionSource.SetCanceled();
                }
                else if (task.IsSuccessful)
                {
                    this.taskCompletionSource.SetResult(task.Result);
                }
                else
                {
                    this.taskCompletionSource.SetException(task.Exception);
                }
            }
        }

        public static Task<Java.Lang.Object> ToAwaitableTask(Android.Gms.Tasks.Task task)
        {
            var taskCompletionSource = new TaskCompletionSource<Java.Lang.Object>();
            var taskCompleteListener = new TaskCompleteListener(taskCompletionSource);
            task.AddOnCompleteListener(taskCompleteListener);

            return taskCompletionSource.Task;
        }

        public async Task<string> ExtractText(Stream image)
        {
            var bitmap = await BitmapFactory.DecodeStreamAsync(image);
           
            var inputImage = InputImage.FromBitmap(bitmap, 0);

            var b = new TextRecognizerOptions.Builder();
            var opts = b.Build();
            var recognizer = TextRecognition.GetClient(opts);
            var t = await ToAwaitableTask(recognizer.Process(inputImage));
            return t.ToString();
        }
       
       
    }
}