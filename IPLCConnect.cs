
namespace common_compolet_pure
{
    public interface IPLCConnect 
    {
        object WriteVar(string name, object value);

        object readFromPlc(string varname);

    }

}
