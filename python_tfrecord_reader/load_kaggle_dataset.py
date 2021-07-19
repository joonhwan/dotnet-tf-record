#%%
from kaggle.api.kaggle_api_extended import KaggleApi

api = KaggleApi()
api.authenticate()

#%%
api.dataset_download_files(
    dataset='wardaddy24/marble-surface-anomaly-detection', 
    path='./.datasets',
    unzip=True
)
print('done')
#%%