#ifndef	__RDF_LTD__ENGINE_INLINE_H
#define	__RDF_LTD__ENGINE_INLINE_H


#include	<assert.h>

#include	"engine.h"


static	inline	int64_t	GetRevision(
								)
{
	char	** timeStamp = nullptr;
	return	GetRevision(
					timeStamp
				);
}

static	inline	int64_t	GetRevisionW(
								)
{
	wchar_t	** timeStamp = nullptr;
	return	GetRevisionW(
					timeStamp
				);
}

static	inline	char	* GetAssertionFile(
								)
{
	char	* fileName = nullptr;
	GetAssertionFile(
			&fileName
		);
	return	fileName;
}

static	inline	wchar_t	* GetAssertionFileW(
								)
{
	wchar_t	* fileName = nullptr;
	GetAssertionFileW(
			&fileName
		);
	return	fileName;
}

static	inline	char	* GetNameOfClass(
									int64_t				owlClass
								)
{
	char	* name = nullptr;
	GetNameOfClass(
			owlClass,
			&name
		);
	return	name;
}

static	inline	wchar_t	* GetNameOfClassW(
									int64_t				owlClass
								)
{
	wchar_t	* name = nullptr;
	GetNameOfClassW(
			owlClass,
			&name
		);
	return	name;
}

static	inline	char	* GetNameOfClassEx(
									int64_t				model,
									int64_t				owlClass
								)
{
	char	* name = nullptr;
	GetNameOfClassEx(
			model,
			owlClass,
			&name
		);
	return	name;
}

static	inline	wchar_t	* GetNameOfClassWEx(
									int64_t				model,
									int64_t				owlClass
								)
{
	wchar_t	* name = nullptr;
	GetNameOfClassWEx(
			model,
			owlClass,
			&name
		);
	return	name;
}

#ifdef NDEBUG
#define AssertInstanceClass (owlInstance,className) /*noop*/
#else
static inline void AssertInstanceClass(int64_t owlInstance, const char* className)
{
	int64_t thisClass = GetInstanceClass(owlInstance);	
	const char* thisClassName = GetNameOfClass(thisClass);
	int64_t expectedClass = GetClassByName(GetModel(owlInstance), className);
	assert(thisClass == expectedClass);
}
#endif


static	inline	char	* GetNameOfProperty(
									int64_t				rdfProperty
								)
{
	char	* name = nullptr;
	GetNameOfProperty(
			rdfProperty,
			&name
		);
	return	name;
}

static	inline	wchar_t	* GetNameOfPropertyW(
									int64_t				rdfProperty
								)
{
	wchar_t	* name = nullptr;
	GetNameOfPropertyW(
			rdfProperty,
			&name
		);
	return	name;
}

static	inline	char	* GetNameOfPropertyEx(
									int64_t				model,
									int64_t				rdfProperty
								)
{
	char	* name = nullptr;
	GetNameOfPropertyEx(
			model,
			rdfProperty,
			&name
		);
	return	name;
}

static	inline	wchar_t	* GetNameOfPropertyWEx(
									int64_t				model,
									int64_t				rdfProperty
								)
{
	wchar_t	* name = nullptr;
	GetNameOfPropertyWEx(
			model,
			rdfProperty,
			&name
		);
	return	name;
}

static	inline	int64_t	CreateInstance(
									int64_t				owlClass
								)
{
	const char	* name = nullptr;
	return	CreateInstance(
					owlClass,
					name
				);
}

static	inline	int64_t	CreateInstanceW(
									int64_t				owlClass
								)
{
	const wchar_t	* name = nullptr;
	return	CreateInstanceW(
					owlClass,
					name
				);
}

static	inline	int64_t	CreateInstanceEx(
									int64_t				model,
									int64_t				owlClass
								)
{
	const char	* name = nullptr;
	return	CreateInstanceEx(
					model,
					owlClass,
					name
				);
}

static	inline	int64_t	CreateInstanceWEx(
									int64_t				model,
									int64_t				owlClass
								)
{
	const wchar_t	* name = nullptr;
	return	CreateInstanceWEx(
					model,
					owlClass,
					name
				);
}

static	inline	char	* GetNameOfInstance(
									int64_t				owlInstance
								)
{
	char	* name = nullptr;
	GetNameOfInstance(
			owlInstance,
			&name
		);
	return	name;
}

static	inline	wchar_t	* GetNameOfInstanceW(
									int64_t				owlInstance
								)
{
	wchar_t	* name = nullptr;
	GetNameOfInstanceW(
			owlInstance,
			&name
		);
	return	name;
}

static	inline	char	* GetNameOfInstanceEx(
									int64_t				model,
									int64_t				owlInstance
								)
{
	char	* name = nullptr;
	GetNameOfInstanceEx(
			model,
			owlInstance,
			&name
		);
	return	name;
}

static	inline	wchar_t	* GetNameOfInstanceWEx(
									int64_t				model,
									int64_t				owlInstance
								)
{
	wchar_t	* name = nullptr;
	GetNameOfInstanceWEx(
			model,
			owlInstance,
			&name
		);
	return	name;
}

static	inline	int64_t	SetDatatypeProperty(
									int64_t				owlInstance,
									int64_t				rdfProperty,
									bool				value
								)
{
	assert(GetPropertyType(rdfProperty) == DATATYPEPROPERTY_TYPE_BOOLEAN);
	const int64_t	card = 1;
	return	SetDatatypeProperty(
					owlInstance,
					rdfProperty,
					(const void*) &value,
					card
				);
}

static	inline	int64_t	SetDatatypeProperty(
									int64_t				owlInstance,
									int64_t				rdfProperty,
									const char			* value
								)
{
	assert(GetPropertyType(rdfProperty) == DATATYPEPROPERTY_TYPE_CHAR);
	const int64_t	card = 1;
	return	SetDatatypeProperty(
					owlInstance,
					rdfProperty,
					(const void*) &value,
					card
				);
}

static	inline	int64_t	SetDatatypeProperty(
									int64_t				owlInstance,
									int64_t				rdfProperty,
									const wchar_t		* value
								)
{
	assert(GetPropertyType(rdfProperty) == DATATYPEPROPERTY_TYPE_CHAR);
	const int64_t	card = 1;
	return	SetDatatypeProperty(
					owlInstance,
					rdfProperty,
					(const void*) &value,
					card
				);
}

static	inline	int64_t	SetDatatypeProperty(
									int64_t				owlInstance,
									int64_t				rdfProperty,
									int64_t				value
								)
{
	assert(GetPropertyType(rdfProperty) == DATATYPEPROPERTY_TYPE_INTEGER);
	const int64_t	card = 1;
	return	SetDatatypeProperty(
					owlInstance,
					rdfProperty,
					(const void*) &value,
					card
				);
}

static	inline	int64_t	SetDatatypeProperty(
									int64_t				owlInstance,
									int64_t				rdfProperty,
									double				value
								)
{
	assert(GetPropertyType(rdfProperty) == DATATYPEPROPERTY_TYPE_DOUBLE);
	const int64_t	card = 1;
	return	SetDatatypeProperty(
					owlInstance,
					rdfProperty,
					(const void*) &value,
					card
				);
}

static	inline	int64_t	SetDatatypeProperty(
									int64_t				owlInstance,
									int64_t				rdfProperty,
									unsigned char		value
								)
{
	assert(GetPropertyType(rdfProperty) == DATATYPEPROPERTY_TYPE_BYTE);
	const int64_t	card = 1;
	return	SetDatatypeProperty(
					owlInstance,
					rdfProperty,
					(const void*) &value,
					card
				);
}

static	inline	int64_t	SetObjectProperty(
									int64_t				owlInstance,
									int64_t				rdfProperty,
									int64_t				value
								)
{
	assert(GetPropertyType(rdfProperty) == OBJECTPROPERTY_TYPE);
	const int64_t	card = 1;
	return	SetObjectProperty(
					owlInstance,
					rdfProperty,
					&value,
					card
				);
}

static	inline	int64_t	CalculateInstance(
									int64_t				owlInstance,
									int64_t				* vertexBufferSize,
									int64_t				* indexBufferSize
								)
{
	int64_t	* transformationBufferSize = nullptr;
	return	CalculateInstance(
					owlInstance,
					vertexBufferSize,
					indexBufferSize,
					transformationBufferSize
				);
}

static	inline	int64_t	GetConceptualFaceEx(
									int64_t				owlInstance,
									int64_t				index,
									int64_t				* startIndexTriangles,
									int64_t				* noIndicesTriangles,
									int64_t				* startIndexLines,
									int64_t				* noIndicesLines,
									int64_t				* startIndexPoints,
									int64_t				* noIndicesPoints
								)
{
	int64_t	* startIndexFacePolygons = nullptr,
			* noIndicesFacePolygons = nullptr,
			* startIndexConceptualFacePolygons = nullptr,
			* noIndicesConceptualFacePolygons = nullptr;
	return	GetConceptualFaceEx(
					owlInstance,
					index,
					startIndexTriangles,
					noIndicesTriangles,
					startIndexLines,
					noIndicesLines,
					startIndexPoints,
					noIndicesPoints,
					startIndexFacePolygons,
					noIndicesFacePolygons,
					startIndexConceptualFacePolygons,
					noIndicesConceptualFacePolygons
				);
}

static	inline	double	GetArea(
									int64_t				owlInstance
								)
{
	const void	* vertexBuffer = nullptr,
				* indexBuffer = nullptr;
	return	GetArea(
					owlInstance,
					vertexBuffer,
					indexBuffer
				);
}

static	inline	double	GetVolume(
									int64_t				owlInstance
								)
{
	const void	* vertexBuffer = nullptr,
				* indexBuffer = nullptr;
	return	GetVolume(
					owlInstance,
					vertexBuffer,
					indexBuffer
				);
}

static	inline	double	* GetCenter(
									int64_t				owlInstance,
									double				* center
								)
{
	const void	* vertexBuffer = nullptr,
				* indexBuffer = nullptr;
	return	GetCenter(
					owlInstance,
					vertexBuffer,
					indexBuffer,
					center
				);
}

static	inline	double	GetCentroid(
									int64_t				owlInstance,
									double				* centroid
								)
{
	const void	* vertexBuffer = nullptr,
				* indexBuffer = nullptr;
	return	GetCentroid(
					owlInstance,
					vertexBuffer,
					indexBuffer,
					centroid
				);
}

static	inline	double	GetConceptualFaceArea(
									int64_t				conceptualFace
								)
{
	const void	* vertexBuffer = nullptr,
				* indexBuffer = nullptr;
	return	GetConceptualFaceArea(
					conceptualFace,
					vertexBuffer,
					indexBuffer
				);
}

static	inline	int32_t	GetColorComponent(
								int64_t					owlInstanceColorComponent
							)
{
	AssertInstanceClass(owlInstanceColorComponent, "ColorComponent");

	int64_t	model = GetModel(owlInstanceColorComponent);

	const char* rgbwNames[4] = {"R","G","B","W"};
	double	    rgbwValues[4] = {0,0,0,0};

	for (int i = 0; i<4; i++)
	{
		double	* values = nullptr;
		int64_t	card = 0;
		GetDatatypeProperty(
				owlInstanceColorComponent,
				GetPropertyByName(
						model,
						rgbwNames[i]
					),
				(void**) &values,
				&card
			);
		assert(card == 1); //color component consistent if all values are set
		rgbwValues[i] = (card == 1) ? values[0] : 0.;
	}

	return	COLOR_ARR_RGBW(rgbwValues);
}

static inline void SetColorComponent (
								int64_t					owlInstanceColorComponent,
								int32_t					color
							)
{
	AssertInstanceClass(owlInstanceColorComponent, "ColorComponent");

	int64_t	model = GetModel(owlInstanceColorComponent);

	const char* rgbwNames[4] = {"R","G","B","W"};
	double      rgbwValues[4];

	COLOR_GET_COMPONENTS(rgbwValues, color);

	for (int i = 0; i < 4; i++) {
		SetDatatypeProperty(
			owlInstanceColorComponent,
			GetPropertyByName(
				model,
				rgbwNames[i]
			),
			&rgbwValues[i],
			1
		);
	}
}


static	inline	void	GetColor(
								int64_t					owlInstanceColor,
								int32_t					* ambient,
								int32_t					* diffuse,
								int32_t					* emissive,
								int32_t					* specular
							)
{
	if (owlInstanceColor) {
		AssertInstanceClass(owlInstanceColor, "Color");

		int64_t	model = GetModel(owlInstanceColor);
		GetDefaultColor(
				model,
				ambient,
				diffuse,
				emissive,
				specular
			);

		const char* componentNames[4] = {"ambient", "diffuse", "emissive", "specular"};
		int32_t* componentColors[4] = {ambient, diffuse, emissive, specular};
		
		for (int i = 0; i < 4; i++) {
			if (componentColors[i]) {
				int64_t* values = nullptr, card = 0;
				GetObjectProperty(
					owlInstanceColor,
					GetPropertyByName(
						model,
						componentNames[i]
					),
					&values,
					&card
				);

				int64_t	owlInstanceColorComponent = (card == 1) ? values[0] : 0;
				if (owlInstanceColorComponent) { //if color component is not set - remains default
					(*componentColors[i]) = GetColorComponent(owlInstanceColorComponent);
				}
			}
		}
	}
}

static	inline	void	GetMaterialColor(
								int64_t					owlInstanceMaterial,
								int32_t					* ambient,
								int32_t					* diffuse,
								int32_t					* emissive,
								int32_t					* specular
							)
{
	AssertInstanceClass(owlInstanceMaterial, "Material");

	int64_t* values = nullptr, card = 0;
	GetObjectProperty(
		owlInstanceMaterial,
		GetPropertyByName(
			GetModel(owlInstanceMaterial),
			(char*) "color"
		),
		&values,
		&card
	);

	int64_t	owlInstanceColor = (card == 1) ? values[0] : 0;
	if (owlInstanceColor) {
		return	GetColor(
			owlInstanceColor,
			ambient,
			diffuse,
			emissive,
			specular
		);
	}
	else {
		GetDefaultColor(
			GetModel(owlInstanceMaterial),
			ambient,
			diffuse,
			emissive,
			specular
		);
	}
}

static inline int32_t GetMaterialColorAmbient(
								int64_t					owlInstanceMaterial
							)
{
	int32_t ret = 0;
	GetMaterialColor(owlInstanceMaterial, &ret, nullptr, nullptr, nullptr);
	return ret;
}

static inline int32_t GetVertexColorAmbient(
								int64_t                model,
								const void* vertexBuffer,
								int64_t                vertexIndex,
								int64_t                setting
							)
{
	int32_t ambient = 0;
	GetVertexColor(model, vertexBuffer, vertexIndex, setting, &ambient, NULL, NULL, NULL);
	return ambient;
}
#endif