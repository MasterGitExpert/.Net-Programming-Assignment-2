using Mapster;

namespace DeliveryLogisticsAndTracking.Components.ViewModels
{
    public abstract class BaseViewModel<TViewModel, TModel> : IRegister
                                                where TViewModel : class, new()
                                                where TModel : class, new()
    {

        public TModel ToModel()
        {
            return this.Adapt<TModel>();
        }

        public TModel ToModel(TModel model)
        {
            return (this as TViewModel).Adapt(model);
        }

        public static TViewModel FromModel(TModel model)
        {
            return model.Adapt<TViewModel>();
        }

        private TypeAdapterConfig Config { get; set; }

        public virtual void AddCustomMappings() { }


        protected TypeAdapterSetter<TViewModel, TModel> SetCustomMappings()
            => Config.ForType<TViewModel, TModel>();

        protected TypeAdapterSetter<TModel, TViewModel> SetCustomMappingsInverse()
            => Config.ForType<TModel, TViewModel>();

        public void Register(TypeAdapterConfig config)
        {
            Config = config;
            AddCustomMappings();
        }
    }
}
