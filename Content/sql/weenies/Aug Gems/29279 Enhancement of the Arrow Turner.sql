DELETE FROM `weenie` WHERE `class_Id` = 29279;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (29279, 'gemaugmentationnaturalresistancepierc', 67, '2022-06-02 11:22:51') /* AugmentationDevice */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (29279,   1,        128) /* ItemType - Misc */
     , (29279,   5,         50) /* EncumbranceVal */
     , (29279,  16,          8) /* ItemUseable - Contained */
     , (29279,  19,         100) /* Value */
     , (29279,  33,          1) /* Bonded - Bonded */
     , (29279,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (29279, 114,          1) /* Attuned - Attuned */
     , (29279, 215,         23) /* AugmentationStat */;

INSERT INTO `weenie_properties_int64` (`object_Id`, `type`, `value`)
VALUES (29279,   3, 2000000000) /* AugmentationCost */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (29279,  11, True ) /* IgnoreCollisions */
     , (29279,  13, True ) /* Ethereal */
     , (29279,  14, True ) /* GravityStatus */
     , (29279,  19, True ) /* Attackable */
     , (29279,  22, True ) /* Inscribable */
     , (29279,  69, False) /* IsSellable */;

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (29279,   1, 'Enhancement of the Arrow Turner') /* Name */
     , (29279,  16, 'Using this gem will grant you 10% extra resistance to Piercing damage. You may only have two resistance augmentations in effect at any time. You may stack this augmentation with another Piercing resistance augmentation.') /* LongDesc */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (29279,   1,   33554809) /* Setup */
     , (29279,   3,  536870932) /* SoundTable */
     , (29279,   8,  100686474) /* Icon */
     , (29279,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2022-06-02T04:16:05.067115-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [
    {
      "created": "0001-01-01T00:00:00",
      "author": "CrimsonMage",
      "comment": "Requires Server Support (Aug stat is 23 )"
    }
  ],
  "UserChangeSummary": "Requires Server Support (Aug stat is 23 )",
  "IsDone": true
}
*/
