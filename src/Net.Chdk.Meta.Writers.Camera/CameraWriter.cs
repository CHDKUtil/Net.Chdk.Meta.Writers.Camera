using Net.Chdk.Meta.Model.Camera;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Chdk.Meta.Writers.Camera
{
    public abstract class CameraWriter<TCamera, TModel, TCard>
        where TCamera : CameraData<TCamera, TModel, TCard>
        where TModel : CameraModelData
        where TCard : CardData
    {
        private IEnumerable<IInnerCameraWriter<TCamera, TModel, TCard>> InnerWriters;

        protected CameraWriter(IEnumerable<IInnerCameraWriter<TCamera, TModel, TCard>> innerWriters)
        {
            InnerWriters = innerWriters;
        }

        public void WriteCameras(string path, IDictionary<string, TCamera> cameras)
        {
            var ext = Path.GetExtension(path);
            var writer = InnerWriters.SingleOrDefault(w => w.Extension.Equals(ext, StringComparison.OrdinalIgnoreCase));
            if (writer == null)
                throw new InvalidOperationException($"Unknown camera writer extension: {ext}");
            writer.WriteCameras(path, cameras);
        }
    }
}
