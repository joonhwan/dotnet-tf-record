#%%
import tensorflow_datasets as tfds
from tensorflow_datasets.core import download
#%%
tfds.list_builders()
#%%
ds = tfds.image_classification.CatsVsDogs()
download_config = tfds.download.DownloadConfig(
    extract_dir='./.datasets/cats-and-dogs/extracted'
)
print(ds)
ds.download_and_prepare(download_dir="./.datasets/cats-and-dogs", download_config=download_config)
#%%
download_config = tfds.download.DownloadConfig(
    extract_dir='./.datasets/cats-and-dogs/extracted',
    download_mode=tfds.GenerateMode.FORCE_REDOWNLOAD,
)
mnist_builder = tfds.builder("mnist")
mnist_info = mnist_builder.info
print(mnist_info)

mnist_builder.download_and_prepare(download_dir="./.datasets/mnist", download_config=download_config)
print('ok')
