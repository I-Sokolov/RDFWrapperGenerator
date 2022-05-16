#include <stdio.h>
#include <math.h>
#include <string.h>
#include <assert.h>


#include "engine.h"
#include "CustomModel.h"
using namespace CustomModel;

int main()
{
    int64_t model = OpenModel("CusomModel.bin");

    MyCustomCylinder cylinder = MyCustomCylinder::Create(model);

    int64_t cnt = 0;
    const char* const* str = cylinder.get_MyCustomString(&cnt);
    assert(str == NULL && cnt == 0);
    
    const bool* flags = cylinder.get_MyCustomBool(&cnt);
    assert(flags == NULL);

    const char* strset[] =  {"S1", "S2"};
    cylinder.set_MyCustomString(strset, 2);
    
    bool flagset[] = { false, true, false };
    cylinder.set_MyCustomBool(flagset, 3);

    str = cylinder.get_MyCustomString(&cnt);

    assert(cnt == 2 && !strcmp (str[0],"S1") && !strcmp (str[1],"S2"));

    flags = cylinder.get_MyCustomBool(&cnt);
    assert(cnt == 3 && !flags[0] && flags[1] && !flags[2]);
    
    CloseModel(model);

    printf("Test finished\n");
}

